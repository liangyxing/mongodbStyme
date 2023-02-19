using Yxing.MongoDBSystem.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Yxing.MongoDBSystem.Permissions;

public class MongoDBSystemPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MongoDBSystemPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(MongoDBSystemPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MongoDBSystemResource>(name);
    }
}
