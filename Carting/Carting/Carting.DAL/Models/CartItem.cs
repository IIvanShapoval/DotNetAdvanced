using LiteDB;

namespace CartingService.Carting.DAL.Models
{
    public class CartItem
    {
        [BsonId]
        public Guid Id { get; }

        public string Name { get; }

        public string? ImageUrl { get; }

        public string? AltText { get; }

        public decimal Price { get; }

        public int Quantity { get; }

        public int CategoryId { get; }

        [BsonCtor]
        public CartItem(Guid _id,
            string name,
            decimal price,
            int quantity,
            int categoryId,
            string? imageUrl = null,
            string? altText = null
            )
        {
            if ((_id == Guid.Empty) || String.IsNullOrEmpty(name) || price < 0 || quantity < 0)
                throw new ArgumentException("Error creating cart item, invalid argument");
            Id = _id;
            Name = name;
            ImageUrl = imageUrl;
            AltText = altText;
            Price = price;
            Quantity = quantity;
            CategoryId = categoryId;
        }
    }
}
