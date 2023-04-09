namespace Catalog.Application.Features.Products.Queries.GetProducts
{
    public class CategoryDto
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
