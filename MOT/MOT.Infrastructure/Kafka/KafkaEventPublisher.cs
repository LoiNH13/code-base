using Application.Core.Abstractions.Messaging;
using Confluent.Kafka;
using Integration.Abstractions;
using Integration.Kafka.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;

namespace MOT.Infrastructure.Kafka;

internal sealed class KafkaEventPublisher : IKafkaEventPublisher, IDisposable
{
    private readonly IProducer<string, string> _producer;
    private readonly ILogger _logger;

    public KafkaEventPublisher(IOptions<KafkaSetting> kafkaSettingOption, ILogger<KafkaEventPublisher> logger)
    {
        _logger = logger;
        var kafkaSetting = kafkaSettingOption.Value;
        _producer = new ProducerBuilder<string, string>(new ProducerConfig
        {
            BootstrapServers = kafkaSetting.BootstrapServers,
            Acks = Acks.All,
            ClientId = Dns.GetHostName(),
            SaslUsername = kafkaSetting.Username,
            SaslPassword = kafkaSetting.Password,
            SaslMechanism = SaslMechanism.Plain,
            SecurityProtocol = SecurityProtocol.SaslPlaintext,
            EnableDeliveryReports = true
        }).Build();
    }

    public async Task PublishAsync(IKafkaEvent kafkaEvent)
    {
        try
        {
            var value = JsonConvert.SerializeObject(kafkaEvent, typeof(IIntegrationEvent), new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            var message = new Message<string, string> { Key = kafkaEvent.GetKey()!, Value = value };
            foreach (var item in kafkaEvent.GetTopics())
            {
                await _producer.ProduceAsync(item, message);
            }
        }
        catch (ProduceException<string, string> ex)
        {
            _logger.LogError(ex, "An exception occurred: {Message}", ex.Message);
        }
    }

    public void Dispose()
    {
        _producer.Dispose();
    }
}