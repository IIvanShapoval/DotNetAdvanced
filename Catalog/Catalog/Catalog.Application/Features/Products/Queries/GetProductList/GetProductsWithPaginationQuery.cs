using AutoMapper;
using Catalog.Application.Contracts.Persistance;
using Catalog.Application.Features.Categories.Queries.GetCategoriesListWithProducts;
using Catalog.Application.Models;
using MediatR;

namespace Catalog.Application.Features.Products.Queries.GetProductList
{
    public record GetProductsWithPaginationQuery : IRequest<PaginatedList<ProductDto>>
    {
        public int CategoryId { get; init; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public class GetProductsWithPaginationQueryHandler : IRequestHandler<GetProductsWithPaginationQuery, PaginatedList<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsWithPaginationQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ProductDto>> Handle(GetProductsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetAllProducts(request);
        }
    }
}
