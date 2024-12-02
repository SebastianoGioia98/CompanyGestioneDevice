using Company.GestioneDevice.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Company.GestioneDevice.Permissions;

public class GestioneDevicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(GestioneDevicePermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(GestioneDevicePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<GestioneDeviceResource>(name);
    }
}
