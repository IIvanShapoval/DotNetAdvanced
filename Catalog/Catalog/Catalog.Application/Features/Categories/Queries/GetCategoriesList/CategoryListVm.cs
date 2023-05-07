namespace Catalog.Application.Features.Categories.Queries.GetCategoriesList
{
    public class CategoryListVm
    {
        public IReadOnlyCollection<CategoryDto> Categories { get; set; }

        public CategoryListVm() { }

        public CategoryListVm(IReadOnlyCollection<CategoryDto> categories)
        {
            Categories = categories;
        }
    }
}
