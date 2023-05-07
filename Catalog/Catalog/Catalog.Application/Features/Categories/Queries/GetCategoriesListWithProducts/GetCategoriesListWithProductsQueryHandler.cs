using AutoMapper;
using Catalog.Application.Contracts.Persistance;
using Catalog.Domain.Entities;
using MediatR;
using System.Collections.ObjectModel;

namespace Catalog.Application.Features.Categories.Queries.GetCategoriesListWithProducts
{
    public class GetCategoriesListWithProductsQueryHandler : IRequestHandler<GetCategoriesListWithProductsQuery, CategoryProductListVm>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoriesListWithProductsQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryProductListVm> Handle(GetCategoriesListWithProductsQuery request, CancellationToken cancellationToken)
        {
            var allCategories = await _categoryRepository.GetCategoriesWithProducts();
            return new CategoryProductListVm
            {
                Categories = new ReadOnlyCollection<CategoriesWithProducrListDto>(allCategories
                .Select(category => _mapper.Map<Category, CategoriesWithProducrListDto>(category))
                .ToList())
            };
        }
    }
}
