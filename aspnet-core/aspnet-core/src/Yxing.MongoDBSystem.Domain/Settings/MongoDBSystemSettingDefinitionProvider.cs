using Volo.Abp.Settings;

namespace Yxing.MongoDBSystem.Settings;

public class MongoDBSystemSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(MongoDBSystemSettings.MySetting1));
    }
}
