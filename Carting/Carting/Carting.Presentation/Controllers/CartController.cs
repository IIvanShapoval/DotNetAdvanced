using CartingService.Carting.BLL.Dtos;
using CartingService.Carting.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.Carting.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartDto>> GetCart(string id)
        {
            var cart = _cartService.GetCartById(id);
            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        [HttpPost("{id}/items")]
        public async Task<ActionResult<CartItemDto>> AddItemToCart(string id, CartItemDto cartItem)
        {
            _cartService.AddCartItem(id, cartItem);
            return CreatedAtAction(nameof(GetItemFromCart), new { id = cartItem.Id }, cartItem);
        }

        [HttpGet("{id}/items/{itemId}")]
        public async Task<ActionResult<CartItemDto>> GetItemFromCart(string id, int itemId)
        {
            var item = _cartService.GetCartItems(id)
                .FirstOrDefault(cartItem => cartItem.Id == itemId);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpDelete("{id}/items/{itemId}")]
        public async Task<IActionResult> RemoveItemFromCart(string id, int itemId)
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
