using Application.Core.Abstractions.Messaging;

namespace Background.Services;

public interface IIntegrationEventConsumer
{
    void Consume(IIntegrationEvent integrationEvent);
}