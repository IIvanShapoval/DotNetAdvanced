using LiteDB;

namespace CartingService.Carting.DAL.Infrastructure
{
    public interface ILiteDbContext
    {
        LiteDatabase Database { get; }
    }
}
