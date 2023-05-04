using Catalog.Domain.Entities;

namespace Catalog.Application.Contracts.Persistance
{
    public interface IProductRepository : IAsyncRepository<Product>
    {
        Task<bool> IsProductNameAndCategoryUnique(string name, int categoryId);
    }
}
