using Catalog.Domain.Entities;

namespace Catalog.Application.Contracts.Persistance
{
    public interface ICategoryRepository : IAsyncRepository<Category>
    {
        Task<List<Category>> GetCategoriesWithProducts();
    }
}
