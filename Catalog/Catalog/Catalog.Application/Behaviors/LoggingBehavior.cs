using Catalog.Application.Features.Products.Commands.UpdateProductCommand;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Catalog.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
            where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            //Request
            _logger.LogInformation($"Handling {typeof(TRequest).Name}");
            Type myType = request.GetType();

            var myTypeisUpdateProduct = myType.Name == typeof(UpdateProductCommand).Name;


            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            
            if(myTypeisUpdateProduct)
            {
                SetCorrelationId(request as UpdateProductCommand, props);
            }

            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(request, null);
                _logger.LogInformation("{Property} : {@Value}", prop.Name, propValue);
            }

            
            var response = await next();
            //Response
            _logger.LogInformation($"Handled {typeof(TResponse).Name}");
            return response;
        }

        private void SetCorrelationId(UpdateProductCommand request, IList<PropertyInfo> props)
        {
           var correlationIdProperty = props.FirstOrDefault(prop => prop.Name == "CorrelationId");

            object correlationIdValue = null;
            if (correlationIdProperty != null)
            {
                correlationIdValue = (PropertyInfo)correlationIdProperty.GetValue(request, null);
            }

            request.CorrelationId = correlationIdValue != null ? correlationIdValue.ToString() : String.Empty;
        }
    }
}
