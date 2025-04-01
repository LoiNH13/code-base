namespace Integration.Abstractions;

public interface IKafkaEventPublisher
{
    Task PublishAsync(IKafkaEvent kafkaEvent);
}