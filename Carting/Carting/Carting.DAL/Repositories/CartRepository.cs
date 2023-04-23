using CartingService.Carting.DAL.Models;
using CartingService.Carting.DAL.Helpers;
using LiteDB;

namespace CartingService.Carting.DAL.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly string _connectionString = @"Carts.db";

        public IList<CartItem> GetItemsFromCart(Guid cartId)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var cartCollection = db.GetCollection<Cart>("carts");
                var cart = cartCollection.Include(x => x.Items).FindById(cartId);
                return cart.Items.ToList();
            }
        }

        public Guid AddItemToCart(Guid cartId, CartItem item)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var cartCollection = db.GetCollection<Cart>("carts");
                var cart = cartCollection.Include(x => x.Items).FindById(cartId);
                if (cart == null)
                {
                   var id = AddCart(cartId, new Cart(cartId), db);
                   cart = cartCollection.FindById(id);
                }
                var existingItem = cart?.Items.FirstOrDefault(x => x.Id == item.Id);
                if (existingItem != null && cart != null)
                {
                    UpdateCartItem(cartCollection, cart, existingItem, 
                        CartItemHelpers.GetNewCartItemFromExistingItem(existingItem, existingItem.Quantity + item.Quantity));
                }
                else
                {
                    cart?.Items.Add(item);
                    _ = cartCollection.Update(cart);
                }
                return cartCollection.Include(x => x.Items).FindById(cartId).Id;
            }
        }

        public bool RemoveItemFromCart(Guid cartId, Guid itemId)
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

        public Cart GetCartById(Guid id)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                var cartCollection = db.GetCollection<Cart>("carts");
                var cart = cartCollection.Include(x => x.Items).FindById(id);
                return cart;
            }
        }

        public Guid AddCart(Guid cartId, Cart cart)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                return AddCart(cartId, cart, db);
            }
        }

        private Guid AddCart(Guid cartId, Cart cart, LiteDatabase db)
        {
            var cartCollection = db.GetCollection<Cart>("carts");
            var existingCart = cartCollection.Include(x => x.Items).FindById(cartId);
            if (existingCart == null)
            {
                return cartCollection.Insert(cart).AsGuid;
            }
            return Guid.Empty;
        }

        private void UpdateCartItem(ILiteCollection<Cart> cartCollection, Cart cart, CartItem existingCartItem, CartItem updatedCartItem)
        {
            var itemIndex = cart.Items.IndexOf(existingCartItem);
            cart.Items[itemIndex] = updatedCartItem;
            _ = cartCollection.Update(cart);
        }
    }
}
