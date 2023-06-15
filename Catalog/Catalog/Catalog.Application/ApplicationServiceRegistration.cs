using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using MediatR;
using Catalog.Application.Behaviors;
    using CorrelationId.DependencyInjection;

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

            services.AddDefaultCorrelationId(options =>
            {
                options.AddToLoggingScope = true;
                options.IgnoreRequestHeader = false;
                options.IncludeInResponse = true;
                options.RequestHeader = "x-correlation-id";
                options.ResponseHeader = "x-correlation-id";
                options.UpdateTraceIdentifier = true;
            });

            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(LoggingBehavior<,>));

            services.AddAzureClients(clientFactoryBuilder =>
                                    clientFactoryBuilder.AddServiceBusClient(
                                       configuration.GetConnectionString("ServiceBus")));

            return services;
        }
    }
}
