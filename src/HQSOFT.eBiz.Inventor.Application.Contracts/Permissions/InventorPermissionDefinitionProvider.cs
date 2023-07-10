using HQSOFT.eBiz.Inventor.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace HQSOFT.eBiz.Inventor.Permissions;

public class InventorPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(InventorPermissions.GroupName, L("Permission:Inventor"));

        var lotSerClassPermission = myGroup.AddPermission(InventorPermissions.LotSerClasses.Default, L("Permission:LotSerClasses"));
        lotSerClassPermission.AddChild(InventorPermissions.LotSerClasses.Create, L("Permission:Create"));
        lotSerClassPermission.AddChild(InventorPermissions.LotSerClasses.Edit, L("Permission:Edit"));
        lotSerClassPermission.AddChild(InventorPermissions.LotSerClasses.Delete, L("Permission:Delete"));

        var lotSerSegmentPermission = myGroup.AddPermission(InventorPermissions.LotSerSegments.Default, L("Permission:LotSerSegments"));
        lotSerSegmentPermission.AddChild(InventorPermissions.LotSerSegments.Create, L("Permission:Create"));
        lotSerSegmentPermission.AddChild(InventorPermissions.LotSerSegments.Edit, L("Permission:Edit"));
        lotSerSegmentPermission.AddChild(InventorPermissions.LotSerSegments.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<InventorResource>(name);
    }
}