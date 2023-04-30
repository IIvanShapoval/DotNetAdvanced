using CartingService.Carting.BLL.Dtos;
using CartingService.Carting.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.Carting.Presentation.Controllers.V2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class CartsController : ControllerBase
    {
        protected readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("{id}/cartItems")]
        public async Task<ActionResult<CartItemDto>> AddItemToCart(Guid id, [FromBody] CartItemDto cartItem)
        {
            _cartService.AddCartItem(id, cartItem);
            return Ok(cartItem);
        }

        [HttpDelete("{id}/items/{itemId}")]
        public async Task<IActionResult> RemoveItemFromCart(Guid id, Guid itemId)
        {
            var result = _cartService.RemoveCartItem(id, itemId);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{id}")]
        public async Task<ActionResult<CartDto>> GetCartInfo(Guid id)
        {
            var cart = _cartService.GetCartById(id);
            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }
    }
}
