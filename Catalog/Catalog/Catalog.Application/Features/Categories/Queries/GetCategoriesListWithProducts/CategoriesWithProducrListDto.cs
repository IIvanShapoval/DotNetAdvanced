using Catalog.Application.Mappings;
using Catalog.Domain.Entities;

namespace Catalog.Application.Features.Categories.Queries.GetCategoriesListWithProducts
{
    public class CategoriesWithProducrListDto : IMapFrom<Category>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? ParentCategoryId { get; set; }
        public ICollection<ProductDto>? Products { get; set; }
    }
}
