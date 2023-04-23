using CartingService.Carting.BLL.Dtos;
using CartingService.Carting.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.Carting.Presentation.Controllers.V2
{
    [Route("api/v1/carts")]
    [ApiController]
    public class CartControllerV2 : CommonController
    {
        public CartControllerV2(ICartService cartService) : base(cartService)
        {
        }

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
