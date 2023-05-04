namespace Catalog.Application.Features.Products.Queries.GetProducts
{
    public class ProductVm
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public CategoryDto Category { get; set; } = default!;

        public decimal Price { get; set; }

        public int Amount { get; set; }
    }
}
