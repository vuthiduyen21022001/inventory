using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace HQSOFT.eBiz.Inventor;

[DependsOn(
    typeof(InventorApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class InventorHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(InventorApplicationContractsModule).Assembly,
            InventorRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<InventorHttpApiClientModule>();
        });
    }
}
