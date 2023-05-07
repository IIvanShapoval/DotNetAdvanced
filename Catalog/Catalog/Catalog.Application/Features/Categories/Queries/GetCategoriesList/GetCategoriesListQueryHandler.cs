using AutoMapper;
using Catalog.Application.Contracts.Persistance;
using Catalog.Domain.Entities;
using MediatR;
using System.Collections.ObjectModel;

namespace Catalog.Application.Features.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, CategoryListVm>
    {
        private readonly IAsyncRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoriesListQueryHandler(IMapper mapper, IAsyncRepository<Category> categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryListVm> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            var allCategories = await _categoryRepository.ListAllAsync();
            return new CategoryListVm
            {
                Categories = new ReadOnlyCollection<CategoryDto>(allCategories
                .Select(category => _mapper.Map<Category, CategoryDto>(category))
                .ToList())
            }; 
        }
    }
}
