using System.Collections.Generic;
using System.Linq;
using CartingService.Carting.DAL.Models;
using LiteDB;

namespace CartingService.Carting.DAL.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly string _connectionString;

        public CartRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Cart GetById(string id)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var cartCollection = db.GetCollection<Cart>("carts");
                var cart = cartCollection.Include(x => x.Items).FindById(id);
                return cart;
            }
        }

        public List<CartItem> GetItems(string cartId)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var cartCollection = db.GetCollection<Cart>("carts");
                var cart = cartCollection.Include(x => x.Items).FindById(cartId);
                return cart.Items.ToList();
            }
        }

        public void AddItem(string cartId, CartItem item)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var cartCollection = db.GetCollection<Cart>("carts");
                var cart = cartCollection.Include(x => x.Items).FindById(cartId);
                if (cart == null)
                {
                    cart = new Cart { Id = cartId, Items = new List<CartItem>() };
                    cartCollection.Insert(cart);
                }
                var existingItem = cart.Items.FirstOrDefault(x => x.Id == item.Id);
                if (existingItem != null)
                {
                    existingItem.Quantity += item.Quantity;
                    cartCollection.Update(cart);
                }
                else
                {
                    cart.Items.Add(item);
                    cartCollection.Update(cart);
                }
            }
        }

        public bool RemoveItem(string cartId, int itemId)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var cartCollection = db.GetCollection<Cart>("carts");
                var cart = cartCollection.Include(x => x.Items).FindById(cartId);
                if (cart != null)
                {
                    var itemToRemove = cart.Items.FirstOrDefault(x => x.Id == itemId);
                    if (itemToRemove != null)
                    {
                        cart.Items.Remove(itemToRemove);
                        cartCollection.Update(cart);
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
