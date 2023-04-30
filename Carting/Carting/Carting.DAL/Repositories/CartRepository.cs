using CartingService.Carting.DAL.Models;
using CartingService.Carting.DAL.Helpers;
using LiteDB;
using CartingService.Carting.DAL.Infrastructure;

namespace CartingService.Carting.DAL.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly LiteDatabase _db;

        public CartRepository(ILiteDbContext liteDbContext)
        {
            _db = liteDbContext.Database;
        }

        public IList<CartItem> GetItemsFromCart(Guid cartId)
        {

            var cartCollection = _db.GetCollection<Cart>("carts");
            var cart = cartCollection.Include(x => x.Items).FindById(cartId);
            return cart.Items.ToList();

        }

        public Guid AddItemToCart(Guid cartId, CartItem item)
        {

            var cartCollection = _db.GetCollection<Cart>("carts");
            var cart = cartCollection.Include(x => x.Items).FindById(cartId);
            if (cart == null)
            {
                var id = AddCart(cartId, new Cart(cartId), _db);
                cart = cartCollection.FindById(id);
            }
            var cartItemsCollection = _db.GetCollection<CartItem>("cartItems");
            var existingItem = cart?.Items.FirstOrDefault(x => x.Id == item.Id);
            if (existingItem != null && cart != null)
            {
                cartItemsCollection.Update(item.Id, 
                    CartItemHelpers.GetNewCartItemFromExistingItem(existingItem, existingItem.Quantity + item.Quantity));
                return item.Id;
            }
            else
            {

                var itemId = cartItemsCollection.Insert(item);
                cart?.Items.Add(CartItemHelpers.GetNewCartItemFromExistingItem(item, itemId.AsGuid));
                _ = cartCollection.Update(cart);
                return itemId;
            }
        }

        public bool RemoveItemFromCart(Guid cartId, Guid itemId)
        {

            var cartCollection = _db.GetCollection<Cart>("carts");
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

        public Cart GetCartById(Guid id)
        {

            var cartCollection = _db.GetCollection<Cart>("carts");
            var cart = cartCollection.Include(x => x.Items).FindById(id);
            return cart;

        }

        public Guid AddCart(Guid cartId, Cart cart)
        {

            return AddCart(cartId, cart, _db);

        }

        private Guid AddCart(Guid cartId, Cart cart, LiteDatabase _db)
        {
            var cartCollection = _db.GetCollection<Cart>("carts");
            var existingCart = cartCollection.Include(x => x.Items).FindById(cartId);
            if (existingCart == null)
            {
                return cartCollection.Insert(cart).AsGuid;
            }
            return Guid.Empty;
        }

        private void UpdateCartItem(ILiteCollection<Cart> cartCollection,
                                    ILiteCollection<CartItem> cartItemsCollection,
                                    Cart cart,
                                    CartItem existingCartItem, 
                                    CartItem updatedCartItem)
        {

        }

    }
}
