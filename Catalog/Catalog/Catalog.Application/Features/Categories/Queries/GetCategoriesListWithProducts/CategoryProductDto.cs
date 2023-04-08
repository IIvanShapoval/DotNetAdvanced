namespace Catalog.Application.Features.Categories.Queries.GetCategoriesListWithProducts
{
    public class CategoryProductDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public DateTime Date { get; set; }
        public Guid CategoryId { get; set; }
    }
}
