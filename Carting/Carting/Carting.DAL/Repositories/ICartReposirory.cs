using CartingService.Carting.DAL.Models;

namespace CartingService.Carting.DAL.Repositories
{
    public interface ICartRepository
    {
        Cart GetById(string id);
        List<CartItem> GetItems(string cartId);
        void AddItem(string cartId, CartItem item);
        bool RemoveItem(string cartId, int itemId);
    }
}
