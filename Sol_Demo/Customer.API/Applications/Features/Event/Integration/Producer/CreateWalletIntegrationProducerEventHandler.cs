using MassTransit;
using Queue.Model;

namespace Customer.API.Applications.Features.Event.Integration.Producer
{
    public class CreateWalletIntegrationProducerEvent : INotification
    {
        public Guid? CustomerIdentifier { get; set; }
    }

    public class CreateWalletIntegrationProducerEventHandler : INotificationHandler<CreateWalletIntegrationProducerEvent>
    {
        private readonly IBus? bus;

        public CreateWalletIntegrationProducerEventHandler(IBus bus)
        {
            this.bus = bus;
        }

        async Task INotificationHandler<CreateWalletIntegrationProducerEvent>.Handle(CreateWalletIntegrationProducerEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var endpoint = await this?.bus?.GetSendEndpoint(new Uri("queue:create-wallet-send-queue"))!;
                await endpoint.Send<CustomerWalletQueueModel>(new CustomerWalletQueueModel()
                {
                    CustomerIdentifier = notification?.CustomerIdentifier!
                });
            }
            catch
            {
                throw;
            }
        }
    }
}