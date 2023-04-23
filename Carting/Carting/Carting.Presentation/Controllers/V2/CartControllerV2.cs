using CartingService.Carting.BLL.Dtos;
using CartingService.Carting.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.Carting.Presentation.Controllers.V1
{
    [Route("api/V2/carts")]
    [ApiController]
    public class CartControllerV2 : CommonController
    {
        public CartControllerV2(ICartService cartService) : base(cartService)
        {
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetCartInfo(Guid id)
        {
            var cartItems = _cartService.GetCartItems(id);
            if (cartItems == null || !cartItems.Any())
            {
                return NotFound();
            }

            return Ok(cartItems);
        }
    }
}
