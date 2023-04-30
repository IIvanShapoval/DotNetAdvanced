using CartingService.Carting.BLL.Dtos;
using CartingService.Carting.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CartingService.Carting.Presentation.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [MapToApiVersion("2.0")]
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
