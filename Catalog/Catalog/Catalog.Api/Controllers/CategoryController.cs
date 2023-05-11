using MediatR;
using Microsoft.AspNetCore.Mvc;
using Catalog.Application.Features.Categories.Commands.CreateCategory;
using Catalog.Application.Features.Categories.Commands.UpdateCategory;
using Catalog.Application.Features.Categories.Commands.DeleteCategory;
using Catalog.Application.Features.Categories.Queries.GetCategoriesList;
using Catalog.Application.Features.Categories.Queries.GetCategoriesListWithProducts;

namespace Catalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all", Name = "GetAllCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CategoryListVm>> GetAllCategories()
        {
            var dtos = await _mediator.Send(new GetCategoriesListQuery());
            return Ok(dtos);
        }

        [HttpGet("allwithproducts", Name = "GetCategoriesWithProducts")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CategoryProductListVm>>> GetCategoriesWithProducts()
        {
            GetCategoriesListWithProductsQuery getCategoriesListWithEventsQuery = new GetCategoriesListWithProductsQuery();

            var dtos = await _mediator.Send(getCategoriesListWithEventsQuery);
            return Ok(dtos);
        }

        [HttpPost(Name = "AddCategory")]
        public async Task<ActionResult<CreateCategoryCommandResponse>> Create([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            var response = await _mediator.Send(createCategoryCommand);
            return Ok(response);
        }

        [HttpPut(Name = "UpdateCategory")]
        public async Task<ActionResult> Update([FromBody] UpdateCategoryCommand updateCategoryCommand)
        {
            await _mediator.Send(updateCategoryCommand);
            return NoContent();
        }

        [HttpDelete(Name = "DeleteCategory")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleteCategoryCommand = new DeleteCategoryCommand() { Id = id };
            await _mediator.Send(deleteCategoryCommand);
            return NoContent();
        }
    }
}
