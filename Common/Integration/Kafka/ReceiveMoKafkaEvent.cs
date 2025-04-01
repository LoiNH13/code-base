using Application.Core.Abstractions.Messaging;
using Integration.Abstractions;
using Integration.Kafka.Settings;
using Newtonsoft.Json;

namespace Integration.Kafka;

public class ReceiveMoKafkaEvent : IIntegrationEvent, IKafkaEvent
{
    private readonly HashSet<string> _topics = new([KafkaTopics.MotService.MoMessage]);

    public ReceiveMoKafkaEvent(
        int moId,
        string telco,
        string serviceNum,
        string phone,
        string syntax,
        string encryptedMessage,
        string signature,
        string moSource,
        string[] topics = default!)
    {
        MoId = moId;
        Telco = telco;
        ServiceNum = serviceNum;
        Phone = phone;
        Syntax = syntax;
        EncryptedMessage = encryptedMessage;
        Signature = signature;
        MoSource = moSource;

        if (topics != null)
        {
            foreach (var item in topics)
            {
                _topics.Add(item);
            }
        }

    }

    public int MoId { get; }

    public string Telco { get; }

    public string ServiceNum { get; }

    public string Phone { get; }

    public string Syntax { get; }

    public string EncryptedMessage { get; }

    public string Signature { get; }

    public string MoSource { get; }

    public string MoResult { get; set; } = "01|Tin nhan khong hop le.";

    public double? MessagePrice { get; set; }

    public string? PartnerResponse { get; set; }

    public long TimeStamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

    public HashSet<string> GetTopics() => _topics;

    public string GetKey() => default!;

    public override string ToString() => JsonConvert.SerializeObject(this);
}