using Catalog.Application.Contracts.Persistance;
using Catalog.Application.Exceptions;
using Catalog.Domain.Entities;
using Catalog.Persistance.Migrations;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Persistance.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(CatalogDbContext dbContext) : base(dbContext)
        {

        }

        public Task<bool> IsProductNameAndCategoryUnique(string name, int categoryId)
        {
            var matches = _dbContext.Products.Any(e => e.Name.Equals(name) && e.CategoryId.Equals(categoryId));
            return Task.FromResult(matches);
        }

        public async Task<Product> AddAsync(Product entity)
        {
            var category = _dbContext.Set<Category>().Find(entity.CategoryId);

            if (category == null)
            {
                throw new NotFoundException("category", entity.CategoryId);
            };

            await _dbContext.Set<Product>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
