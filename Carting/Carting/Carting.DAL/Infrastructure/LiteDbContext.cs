using CartingService.Carting.DAL.Helpers;
using LiteDB;
using Microsoft.Extensions.Options;

namespace CartingService.Carting.DAL.Infrastructure
{
    public class LiteDbContext : ILiteDbContext
    {
        public LiteDatabase Database { get; }

        public LiteDbContext(IOptions<LiteDbOptions> options)
        {
            Database = new LiteDatabase(options.Value.DatabaseLocation, CartBsonMapperHelper.GetCartMappings());
        }
    }
}
