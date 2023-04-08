using Catalog.Application.Contracts.Persistance;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Persistance.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(CatalogDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Category>> GetCategoriesWithProducts()
        {
            var allCategories = await _dbContext.Categories.Include(x => x.Products).ToListAsync();
            return allCategories;
        }
    }
}
