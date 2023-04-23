using CartingService.Carting.BLL.Dtos;

namespace CartingService.Carting.BLL.Services
{
    public interface ICartService
    {
        CartDto GetCartById(Guid id);
        Guid AddCartItem(Guid cartId, CartItemDto itemDto);
        IEnumerable<CartItemDto> GetCartItems(Guid cartId);
        bool RemoveCartItem(Guid cartId, Guid itemId);
    }
}