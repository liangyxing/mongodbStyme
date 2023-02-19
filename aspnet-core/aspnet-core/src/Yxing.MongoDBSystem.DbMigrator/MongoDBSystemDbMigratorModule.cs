using Yxing.MongoDBSystem.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Yxing.MongoDBSystem.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(MongoDBSystemEntityFrameworkCoreModule),
    typeof(MongoDBSystemApplicationContractsModule)
    )]
public class MongoDBSystemDbMigratorModule : AbpModule
{

}
