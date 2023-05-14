using AutoMapper;
using AutoMapper.QueryableExtensions;
using Catalog.Application.Contracts.Persistance;
using Catalog.Application.Exceptions;
using Catalog.Application.Features.Categories.Queries.GetCategoriesListWithProducts;
using Catalog.Application.Features.Products.Queries.GetProductList;
using Catalog.Application.Mappings;
using Catalog.Application.Models;
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
        private readonly IMapper _mapper;

        public ProductRepository(CatalogDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
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

        public async Task<PaginatedList<ProductDto>> GetAllProducts(GetProductsWithPaginationQuery request)
        {
            return await _dbContext.Products
                .Where(product => product.CategoryId == request.CategoryId)
                .OrderBy(product => product.Name)
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
