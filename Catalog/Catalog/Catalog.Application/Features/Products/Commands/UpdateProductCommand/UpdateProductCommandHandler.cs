using AutoMapper;
using Azure.Messaging.ServiceBus;
using Catalog.Application.Contracts.Persistance;
using Catalog.Application.Exceptions;
using Catalog.Domain.Entities;
using MediatR;
using System.Reflection;
using System.Text.Json;
using CorrelationId.Abstractions;

namespace Catalog.Application.Features.Products.Commands.UpdateProductCommand
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IAsyncRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ICorrelationContextAccessor _correlationContextAccessor;

        public UpdateProductCommandHandler(IMapper mapper,
            IAsyncRepository<Product> productRepository,
            ServiceBusClient serviceBusClient,
            ICorrelationContextAccessor correlationContextAccessor)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _serviceBusClient = serviceBusClient;
            _correlationContextAccessor = correlationContextAccessor;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {

            var productToUpdate = await _productRepository.GetByIdAsync(request.Id);
            if (productToUpdate == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            var validator = new UpdateProductCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            _mapper.Map(request, productToUpdate, typeof(UpdateProductCommand), typeof(Product));

            await _productRepository.UpdateAsync(productToUpdate);
            await SendUpdateMessage(productToUpdate);
            return true;
        }


        private async Task SendUpdateMessage(Product product)
        {
            var sender = _serviceBusClient.CreateSender("update-catalog-data");
            var body = JsonSerializer.Serialize(product);
            
            var message = new ServiceBusMessage(body);

            message.CorrelationId = _correlationContextAccessor.CorrelationContext.CorrelationId;
            
            if (body.Contains("scheduled"))
                message.ScheduledEnqueueTime = DateTimeOffset.UtcNow.AddSeconds(15);

            if (body.Contains("ttl"))
                message.TimeToLive = TimeSpan.FromSeconds(20);

            await sender.SendMessageAsync(message);
        }

       
    }
}

