using MassTransit;

namespace Customer.API.Extensions
{
    public static class RabbitmqExtension
    {
        public static void AddRabbitMQ(this IServiceCollection services)
        {
            services.AddMassTransit((config) =>
            {
                config.AddBus((busFactory) => Bus.Factory.CreateUsingRabbitMq((configRabbitMq) =>
                {
                    configRabbitMq.Host(new Uri("rabbitmq://host.docker.internal"), (configHost) =>
                    {
                        configHost.Username("guest");
                        configHost.Password("guest");
                    });
                }));
            });
        }
    }
}