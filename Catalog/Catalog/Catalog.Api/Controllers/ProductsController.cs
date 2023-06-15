using Catalog.Application.Features.Products.Commands.CreateProduct;
using Catalog.Application.Features.Products.Commands.UpdateProductCommand;
using Catalog.Application.Features.Products.Commands.DeleteProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Catalog.Application.Features.Products.Queries.GetProductList;
using Catalog.Application.Models;
using Catalog.Application.Features.Categories.Queries.GetCategoriesListWithProducts;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator) => _mediator = mediator;

        [HttpGet(Name = "GetProducts")]
        public async Task<ActionResult<PaginatedList<ProductDto>>> GetProducts([FromQuery] GetProductsWithPaginationQuery getProductsWithPaginationQuery)
        {
           return await _mediator.Send(getProductsWithPaginationQuery);
        }

        [HttpPost(Name = "AddProduct")]
        public async Task<ActionResult<int>> Create([FromBody] CreateProductCommand createProductCommand)
        {
            var id = await _mediator.Send(createProductCommand);
            return Ok(id);
        }

        [HttpPut(Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateProductCommand updateProductCommand)
        {
            await _mediator.Send(updateProductCommand);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            var deleteProductCommand = new DeleteProductCommand() { Id = id };
            await _mediator.Send(deleteProductCommand);
            return NoContent();
        }
    }
}
