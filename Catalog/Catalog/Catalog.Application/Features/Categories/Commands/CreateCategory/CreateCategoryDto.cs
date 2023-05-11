using Catalog.Application.Mappings;
using Catalog.Domain.Entities;

namespace Catalog.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryDto : IMapFrom<Category>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
    }
}
