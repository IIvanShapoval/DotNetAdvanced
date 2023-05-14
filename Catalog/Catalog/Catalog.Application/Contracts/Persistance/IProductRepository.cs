using Catalog.Application.Features.Categories.Queries.GetCategoriesListWithProducts;
using Catalog.Application.Features.Products.Queries.GetProductList;
using Catalog.Application.Models;
using Catalog.Domain.Entities;

namespace Catalog.Application.Contracts.Persistance
{
    public interface IProductRepository : IAsyncRepository<Product>
    {
        Task<bool> IsProductNameAndCategoryUnique(string name, int categoryId);
        Task<PaginatedList<ProductDto>> GetAllProducts(GetProductsWithPaginationQuery query);

    }
}
