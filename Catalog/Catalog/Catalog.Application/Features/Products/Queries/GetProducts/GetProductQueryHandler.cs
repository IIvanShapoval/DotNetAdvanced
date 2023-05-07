using AutoMapper;
using Catalog.Application.Contracts.Persistance;
using Catalog.Application.Exceptions;
using Catalog.Application.Features.Categories.Queries.GetCategoriesList;
using Catalog.Domain.Entities;
using MediatR;

namespace Catalog.Application.Features.Products.Queries.GetProducts
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, List<ProductVm>>
    {
        private readonly IAsyncRepository<Product> _productRepository;
        private readonly IAsyncRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IMapper mapper, 
            IAsyncRepository<Product> productRepository, 
            IAsyncRepository<Category> categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductVm>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            var productDto = _mapper.Map<ProductVm>(product);

            var category = await _categoryRepository.GetByIdAsync(product.CategoryId);

            if (category == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            productDto.Category = _mapper.Map<CategoryDto>(category);

            return new List<ProductVm>() { productDto };
        }
    }
}
