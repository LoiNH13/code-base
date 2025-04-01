using Contract.Common;
using Domain.Core.Primitives.Maybe;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MOT.Application.Core.Abstractions.DxSms;
using MOT.Contract.Core.DxSms;
using RestSharp;

namespace MOT.Infrastructure.DxSms;

internal sealed class SmsService(IConfiguration configuration, ILogger<SmsService> logger) : ISmsService
{
    private readonly RestClient _restClient = new(configuration["DxSms"]!);

    public async Task<Maybe<ResMoResponse>> GetResMoByServicePhone(string servicePhone,
        CancellationToken cancellationToken = default!)
    {
        var @params = new GetResMosRequest(1, 1, servicePhone);
        var request = new RestRequest(@params.GetPath(), Method.Get);

        var response = await _restClient.ExecuteAsync<PagingJson<ResMoResponse>>(request, cancellationToken);
        if (response.IsSuccessful)
        {
            return response.Data!.Items.FirstOrDefault() ?? default!;
        }
        else
        {
            logger.LogError("Failed to get ResMo by service phone. {Error}", response.ErrorMessage);
            return Maybe<ResMoResponse>.None;
        }
    }
}