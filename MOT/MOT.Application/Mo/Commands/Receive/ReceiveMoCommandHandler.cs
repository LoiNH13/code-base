using Application.Core.Abstractions.Messaging;
using Domain.Core.Primitives.Maybe;
using Integration.Abstractions;
using Integration.Kafka;
using Integration.Kafka.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOT.Application.Core.Abstractions.DxSms;
using MOT.Application.Core.Abstractions.VietmapLive;
using MOT.Contract.Core.FTI;
using MOT.Contract.Core.VietmapLive;
using System.Security.Cryptography;
using System.Text;

namespace MOT.Application.Mo.Commands.Receive;

public sealed class ReceiveMoCommandHandler(
    IVietmapLiveService vietmapLiveService,
    ILogger<ReceiveMoCommandHandler> logger,
    ISmsService smsService,
    IOptions<FtiSetting> ftiSettingOptions,
    IKafkaEventPublisher kafkaEventPublisher)
    : ICommandHandler<ReceiveMoCommand, string>
{
    private readonly FtiSetting _ftiSetting = ftiSettingOptions.Value;

    public async Task<string> Handle(ReceiveMoCommand request, CancellationToken cancellationToken)
    {
        var @event = new ReceiveMoKafkaEvent(request.MoId, request.Telco, request.ServiceNum, request.Phone,
            request.Syntax, request.EncryptedMessage, request.Signature, request.MoSource, [KafkaTopics.MotService.IotHubOtpSms]);
        try
        {
            var taskMbResMo = smsService.GetResMoByServicePhone(request.ServiceNum, cancellationToken);

            //send request to web hook vml to check validation
            if (ValidateRequest(request))
            {
                var partnerResponse =
                    await vietmapLiveService.PartnerVerify(new VerifyRequest(request.ServiceNum, request.Phone,
                        request.Syntax));
                @event.PartnerResponse = partnerResponse.Value.ToString();
                @event.MoResult = partnerResponse.Value.MoResponse();
            }
            else
            {
                @event.MoResult = "01|Invalid signature";
            }

            @event.MessagePrice = await taskMbResMo.Match(x => x.PricePerMo,
                () => default!);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in ReceiveMoCommandHandler.Handle");
        }

        await kafkaEventPublisher.PublishAsync(@event);
        //return
        return @event.MoResult;
    }

    private static string DecryptAndToLower(string encryptedMessage)
    {
        // Decode Base64
        var decodedBytes = Convert.FromBase64String(encryptedMessage);
        var decodedMessage = Encoding.UTF8.GetString(decodedBytes);

        // Convert to lowercase
        return decodedMessage.ToLower();
    }

    private static string ComputeSha1(string input)
    {
        using SHA1 sha1 = SHA1.Create();
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes = sha1.ComputeHash(inputBytes);

        // Convert hash to Base64 string
        return Convert.ToBase64String(hashBytes);
    }

    private bool ValidateRequest(ReceiveMoCommand request)
    {
        // Decrypt message and convert to lowercase
        string message = DecryptAndToLower(request.EncryptedMessage);

        // Concatenate data for hash computation
        string dataToHash = request.MoId + request.ServiceNum + request.Phone + message + _ftiSetting.PrivateKey;

        // Compute SHA1 hash
        string computedHash = ComputeSha1(dataToHash);

        // Compare hashes
        if (computedHash != request.Signature)
            return false;

        return true;
    }
}