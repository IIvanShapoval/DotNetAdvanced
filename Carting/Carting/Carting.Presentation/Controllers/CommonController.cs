using CartingService.Carting.BLL.Dtos;
using CartingService.Carting.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.Carting.Presentation.Controllers
{
    [ApiController]
    public abstract class CommonController : ControllerBase
    {
        protected readonly ICartService _cartService;

        public CommonController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("{id}/cartItems")]
        public async Task<ActionResult<CartItemDto>> AddItemToCart(Guid id, [FromBody]CartItemDto cartItem)
        {
            _cartService.AddCartItem(id, cartItem);
            return Ok();
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
    }
}
