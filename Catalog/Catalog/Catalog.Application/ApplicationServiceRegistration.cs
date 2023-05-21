using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;

namespace Catalog.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            //services.AddAzureClients(builder =>
            //{
            //    builder.AddClient<ServiceBusClient, ServiceBusClientOptions>((_, _, _) =>
            //    {
            //        return new ServiceBusClient("ivansandbox.servicebus.windows.net", new DefaultAzureCredential());
            //    });
            //});

            services.AddAzureClients(clientFactoryBuilder =>
                                    clientFactoryBuilder.AddServiceBusClient(
                                       configuration.GetConnectionString("ServiceBus")));

            return services;
        }
    }
}
