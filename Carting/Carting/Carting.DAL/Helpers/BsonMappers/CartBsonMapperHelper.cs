using CartingService.Carting.BLL.Dtos;
using CartingService.Carting.DAL.Models;
using LiteDB;

namespace CartingService.Carting.DAL.Helpers
{
    public static class CartBsonMapperHelper
    {
        public static BsonMapper GetCartMappings()
        {
            var mapper = new BsonMapper();

            mapper.Entity<CartItem>()
                .Id(x => x.Id, true);

            mapper.Entity<Cart>()
                .Id(x => x.Id, false)
                .DbRef(x => x.Items, "cartItems");

            mapper.SerializeNullValues = true;

            return mapper;
        }
    }
}
