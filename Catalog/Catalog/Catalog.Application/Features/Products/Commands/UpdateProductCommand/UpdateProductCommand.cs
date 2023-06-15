using MediatR;
using System.Net.Http.Headers;

namespace Catalog.Application.Features.Products.Commands.UpdateProductCommand
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Image { get; set; }

        public int CategoryId { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }

        public string? ImageUrl { get; set; }
    }
}
