using Volo.Abp.Modularity;

namespace Yxing.MongoDBSystem;

[DependsOn(
    typeof(MongoDBSystemApplicationModule),
    typeof(MongoDBSystemDomainTestModule)
    )]
public class MongoDBSystemApplicationTestModule : AbpModule
{

}
