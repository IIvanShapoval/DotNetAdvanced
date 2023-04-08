namespace Catalog.Application.Features.Categories.Queries.GetCategoriesListWithProducts
{
    public class CategoryProductListVm
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<CategoryProductDto>? Events { get; set; }
    }
}
