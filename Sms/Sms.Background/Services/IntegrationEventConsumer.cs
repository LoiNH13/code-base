using Application.Core.Abstractions.Messaging;
using Background.Services;
using MediatR;

namespace Sms.Background.Services;

public sealed class IntegrationEventConsumer(IMediator mediator) : IIntegrationEventConsumer
{
    public void Consume(IIntegrationEvent integrationEvent)
    {
        mediator.Publish(integrationEvent).GetAwaiter().GetResult();
    }
}