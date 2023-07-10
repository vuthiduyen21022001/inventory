using Localization.Resources.AbpUi;
using HQSOFT.eBiz.Inventor.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace HQSOFT.eBiz.Inventor;

[DependsOn(
    typeof(InventorApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class InventorHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(InventorHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<InventorResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
