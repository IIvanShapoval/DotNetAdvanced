using MediatR;

namespace Catalog.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest
    {
        public int CategoryId { get; set;}
        public string Name { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
    }
}
