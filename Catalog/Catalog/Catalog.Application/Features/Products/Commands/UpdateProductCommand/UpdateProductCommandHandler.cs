using AutoMapper;
using Catalog.Application.Contracts.Persistance;
using Catalog.Application.Exceptions;
using Catalog.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Products.Commands.UpdateProductCommand
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
            private readonly IAsyncRepository<Product> _productRepository;
            private readonly IMapper _mapper;

            public UpdateProductCommandHandler(IMapper mapper, IAsyncRepository<Product> productRepository)
            {
                _mapper = mapper;
                _productRepository = productRepository;
            }

            public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
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
            }
        }
 }

