using MediatR;

namespace Catalog.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Image { get; set; }

        public Guid CategoryId { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }

        public string? ImageUrl { get; set; }

        public override string ToString()
        {
            return $"Product name: {Name}; Price: {Price}; Description: {Description}";
        }
    }
}
