using MediatR;

namespace Catalog.Application.Features.Categories.Queries.GetCategoriesListWithProducts
{
    public class GetCategoriesListWithProductsQuery : IRequest<CategoryProductListVm>
    {
    }
}
