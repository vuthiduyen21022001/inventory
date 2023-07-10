using HQSOFT.eBiz.Inventor.Permissions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using HQSOFT.eBiz.Inventor.Localization;
using Volo.Abp.UI.Navigation;

namespace HQSOFT.eBiz.Inventor.Blazor.Menus;

public class InventorMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }

        var moduleMenu = AddModuleMenuItem(context);
        AddMenuItemLotSerClasses(context, moduleMenu);

        AddMenuItemLotSerSegments(context, moduleMenu);
    }

    private static async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        var l = context.GetLocalizer<InventorResource>();

        context.Menu.AddItem(new ApplicationMenuItem(InventorMenus.Prefix, displayName: "Sample Page", "/Inventor", icon: "fa fa-globe"));

        await Task.CompletedTask;
    }

    private static ApplicationMenuItem AddModuleMenuItem(MenuConfigurationContext context)
    {
        var moduleMenu = new ApplicationMenuItem(
            InventorMenus.Prefix,
            context.GetLocalizer<InventorResource>()["Menu:Inventor"],
            icon: "fa fa-folder"
        );

        context.Menu.Items.AddIfNotContains(moduleMenu);
        return moduleMenu;
    }
    private static void AddMenuItemLotSerClasses(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        parentMenu.AddItem(
            new ApplicationMenuItem(
                Menus.InventorMenus.LotSerClasses,
                context.GetLocalizer<InventorResource>()["Menu:LotSerClasses"],
                "/Inventor/LotSerClasses",
                icon: "fa fa-file-alt",
                requiredPermissionName: InventorPermissions.LotSerClasses.Default
            )
        );
    }

    private static void AddMenuItemLotSerSegments(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        parentMenu.AddItem(
            new ApplicationMenuItem(
                Menus.InventorMenus.LotSerSegments,
                context.GetLocalizer<InventorResource>()["Menu:LotSerSegments"],
                "/Inventor/LotSerSegments",
                icon: "fa fa-file-alt",
                requiredPermissionName: InventorPermissions.LotSerSegments.Default
            )
        );
    }
}