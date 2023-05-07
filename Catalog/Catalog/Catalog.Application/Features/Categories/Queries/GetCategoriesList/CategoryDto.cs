using Catalog.Application.Mappings;
using Catalog.Domain.Entities;

namespace Catalog.Application.Features.Categories.Queries.GetCategoriesList
{
    public class CategoryDto : IMapFrom<Category>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
