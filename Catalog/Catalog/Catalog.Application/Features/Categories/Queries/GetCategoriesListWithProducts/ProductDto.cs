using Catalog.Application.Mappings;
using Catalog.Domain.Entities;

namespace Catalog.Application.Features.Categories.Queries.GetCategoriesListWithProducts
{
    public class ProductDto : IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
    }
}
