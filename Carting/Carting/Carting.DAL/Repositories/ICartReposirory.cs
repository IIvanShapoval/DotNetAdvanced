using CartingService.Carting.BLL.Dtos;
using CartingService.Carting.DAL.Models;
using LiteDB;

namespace CartingService.Carting.DAL.Repositories
{
    public interface ICartRepository
    {
        Cart GetCartById(Guid id);
        IList<CartItem> GetItemsFromCart(Guid cartId);
        Guid AddItemToCart(Guid cartId, CartItem item);
        bool RemoveItemFromCart(Guid cartId, Guid itemId);
        Guid AddCart(Guid cartId, Cart cart);
    }
}
