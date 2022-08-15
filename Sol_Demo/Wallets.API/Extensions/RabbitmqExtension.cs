using Wallets.API.Applications.Features.Event.Integration.Consumer;

namespace Wallets.API.Extensions
{
    public static class RabbitmqExtension
    {
        public static void AddRabbitMQ(this IServiceCollection services)
        {
            services.AddMassTransit((config) =>
            {
                config.AddConsumer<CreateWalletIntegrationConsumerEventHandler>();
                config.AddBus((busFactory) => Bus.Factory.CreateUsingRabbitMq((configRabbitMq) =>
                {
                    configRabbitMq.Host(new Uri("rabbitmq://host.docker.internal"), (configHost) =>
                    {
                        configHost.Username("guest");
                        configHost.Password("guest");
                    });

                    configRabbitMq.ReceiveEndpoint("create-wallet-send-queue", (configReceiveEndPoint) =>
                    {
                        configReceiveEndPoint.PrefetchCount = 16;
                        configReceiveEndPoint.UseMessageRetry(r => r.Interval(2, 100));
                        configReceiveEndPoint.ConfigureConsumer<CreateWalletIntegrationConsumerEventHandler>(busFactory);
                    });
                }));
            });
        }
    }
}