using Application.Core.Abstractions.Messaging;
using Background.Services;
using Confluent.Kafka;
using Integration.Kafka.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Sms.Background.Tasks;

internal sealed class KafkaEventConsumerHostedService : IHostedService, IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger _logger;
    private readonly IConsumer<string, string> _consumer;
    private Task? _executingTask;
    private CancellationTokenSource? _cts;

    public KafkaEventConsumerHostedService(IServiceProvider serviceProvider,
        ILogger<KafkaEventConsumerHostedService> logger, IOptions<KafkaSetting> options)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        var kafkaSetting = options.Value;
        var config = new ConsumerConfig
        {
            BootstrapServers = kafkaSetting.BootstrapServers,
            GroupId = kafkaSetting.Group,
            SaslUsername = kafkaSetting.Username,
            SaslPassword = kafkaSetting.Password,
            SecurityProtocol = SecurityProtocol.SaslPlaintext,
            SaslMechanism = SaslMechanism.Plain,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        _consumer = new ConsumerBuilder<string, string>(config).Build();
    }


    public Task StartAsync(CancellationToken cancellationToken)
    {
        _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        _executingTask = Task.Run(() => StartConsuming(_cts.Token), _cts.Token);

        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_executingTask == null) return;

        await _cts!.CancelAsync(); // Hủy tác vụ đang chạy
        await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
    }

    public void Dispose()
    {
        _consumer.Dispose();
        _cts?.Dispose();
    }

    private async Task StartConsuming(CancellationToken ctsToken)
    {
        _consumer.Subscribe(KafkaTopics.MotService.MoMessage);
        _logger.LogInformation("Kafka consumer started listening to topic: {Topic}", KafkaTopics.MotService.MoMessage);
        try
        {
            while (!ctsToken.IsCancellationRequested)
                try
                {
                    var result = _consumer.Consume(ctsToken);

                    await HandleMessage(result.Message.Value);
                }
                catch (ConsumeException ex)
                {
                    _logger.LogError(ex, "Error consuming message: {Reason}", ex.Error.Reason);
                }
        }
        catch (OperationCanceledException exception)
        {
            _logger.LogInformation(exception, "Kafka consumer stopping...{Message}", exception.Message);
        }
        finally
        {
            _consumer.Close(); // Đóng consumer khi thoát
        }
    }

    private Task HandleMessage(string messageValue)
    {
        var integrationEvent = JsonConvert.DeserializeObject<IIntegrationEvent>(messageValue, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        });

        using var scope = _serviceProvider.CreateScope();
        var integrationEventConsumer = scope.ServiceProvider.GetRequiredService<IIntegrationEventConsumer>();

        integrationEventConsumer.Consume(integrationEvent!);
        return Task.CompletedTask;
    }
}