using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Yxing.MongoDBSystem.Data;

/* This is used if database provider does't define
 * IMongoDBSystemDbSchemaMigrator implementation.
 */
public class NullMongoDBSystemDbSchemaMigrator : IMongoDBSystemDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
