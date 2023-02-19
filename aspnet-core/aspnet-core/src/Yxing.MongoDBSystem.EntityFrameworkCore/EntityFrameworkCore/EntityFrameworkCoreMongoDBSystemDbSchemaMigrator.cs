using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Yxing.MongoDBSystem.Data;
using Volo.Abp.DependencyInjection;

namespace Yxing.MongoDBSystem.EntityFrameworkCore;

public class EntityFrameworkCoreMongoDBSystemDbSchemaMigrator
    : IMongoDBSystemDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreMongoDBSystemDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the MongoDBSystemDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<MongoDBSystemDbContext>()
            .Database
            .MigrateAsync();
    }
}
