using MediatR;

namespace Catalog.Application.Features.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQuery : IRequest<CategoryListVm>
    {
    }
}
