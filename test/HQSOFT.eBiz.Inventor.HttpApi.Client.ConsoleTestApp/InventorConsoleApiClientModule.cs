using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace HQSOFT.eBiz.Inventor;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(InventorHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class InventorConsoleApiClientModule : AbpModule
{

}
