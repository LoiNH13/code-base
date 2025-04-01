using Application.Core.Abstractions.Data;
using Background.Abstractions.Messaging;
using Integration.Kafka;
using Sms.Domain.Entities;
using Sms.Domain.Repositories;

namespace Sms.Background.IntegrationEvents;

public class ReceiveMoKafkaEventHandler(
    IUnitOfWork unitOfWork,
    IMoMessageRepository moMessageRepository,
    IResMoRepository resMoRepository)
    : IIntegrationEventHandler<ReceiveMoKafkaEvent>
{
    public async Task Handle(ReceiveMoKafkaEvent notification, CancellationToken cancellationToken)
    {
        var mbResMo = await resMoRepository.GetByServiceNum(notification.ServiceNum);

        MoMessage moMessage = new MoMessage(notification.MoId, notification.Telco, notification.ServiceNum,
            notification.Phone, notification.Syntax, notification.EncryptedMessage, notification.Signature,
            notification.ToString(), notification.PartnerResponse, notification.MoSource,
            mbResMo.HasValue ? mbResMo.Value.Id : null);

        moMessageRepository.Insert(moMessage);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}