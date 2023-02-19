using System.Threading.Tasks;

namespace Yxing.MongoDBSystem.Data;

public interface IMongoDBSystemDbSchemaMigrator
{
    Task MigrateAsync();
}
