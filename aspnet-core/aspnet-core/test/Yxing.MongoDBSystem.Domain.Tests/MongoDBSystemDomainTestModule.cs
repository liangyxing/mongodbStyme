using Yxing.MongoDBSystem.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Yxing.MongoDBSystem;

[DependsOn(
    typeof(MongoDBSystemEntityFrameworkCoreTestModule)
    )]
public class MongoDBSystemDomainTestModule : AbpModule
{

}
