using Queue.Model;
using Wallets.API.Infrastructures.Dataservice.Command;

namespace Wallets.API.Applications.Features.Event.Integration.Consumer
{
    public class CreateWalletIntegrationConsumerEventHandler : IConsumer<CustomerWalletQueueModel>
    {
        private readonly IMediator mediator;

        public CreateWalletIntegrationConsumerEventHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        async Task IConsumer<CustomerWalletQueueModel>.Consume(ConsumeContext<CustomerWalletQueueModel> context)
        {
            await this.mediator.Publish<CreateWalletDataServiceCommand>(new CreateWalletDataServiceCommand()
            {
                CustomerIdentifier = context.Message.CustomerIdentifier
            });
        }
    }
}