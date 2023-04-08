using MediatR;

namespace Catalog.Application.Features.Categories.Queries.GetCategoriesListWithProducts
{
    public class GetCategoriesListWithProductsQueryHandler : IRequest<List<CategoryProductListVm>>
    {
    }
}
