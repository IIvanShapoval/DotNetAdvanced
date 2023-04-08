using MediatR;
using System.Net.Http.Headers;

namespace Catalog.Application.Features.Products.Commands.UpdateProductCommand
{
    public class UpdateProductCommand : IRequest
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Image { get; set; }

        public Guid CategoryId { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }

        public string? ImageUrl { get; set; }
    }
}
