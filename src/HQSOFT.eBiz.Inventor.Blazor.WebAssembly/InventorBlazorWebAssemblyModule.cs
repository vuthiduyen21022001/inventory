using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace HQSOFT.eBiz.Inventor.Blazor.WebAssembly;

[DependsOn(
    typeof(InventorBlazorModule),
    typeof(InventorHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
)]
public class InventorBlazorWebAssemblyModule : AbpModule
{

}
