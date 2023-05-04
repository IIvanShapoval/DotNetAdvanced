namespace Catalog.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
    }
}
