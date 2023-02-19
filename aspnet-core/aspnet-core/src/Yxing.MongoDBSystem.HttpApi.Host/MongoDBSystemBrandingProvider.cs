using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Yxing.MongoDBSystem;

[Dependency(ReplaceServices = true)]
public class MongoDBSystemBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "MongoDBSystem";
}
