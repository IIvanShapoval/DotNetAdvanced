using CartingService.Carting.BLL.Dtos;
using CartingService.Carting.DAL.Models;

namespace CartingService.Carting.BLL.Services
{
    public interface ICartService
    {
        CartDto GetCartById(string id);
        void AddCartItem(string cartId, CartItemDto itemDto);
        IEnumerable<CartItemDto> GetCartItems(string cartId);
        bool RemoveCartItem(string cartId, int itemId);
    }
}