using LiteDB;

namespace CartingService.Carting.DAL.Models
{
    public class Cart
    {
        [BsonId]
        public Guid Id { get; }

        [BsonRef("cartItems")]
        public IList<CartItem> Items { get; }

        public Cart(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException($"Guid {id} is invalid");
            }
            Id = id;
            Items = new List<CartItem>();
        }
    }
}
