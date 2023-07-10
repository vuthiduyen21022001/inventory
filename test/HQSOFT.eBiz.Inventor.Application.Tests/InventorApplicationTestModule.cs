using Volo.Abp.Modularity;

namespace HQSOFT.eBiz.Inventor;

[DependsOn(
    typeof(InventorApplicationModule),
    typeof(InventorDomainTestModule)
    )]
public class InventorApplicationTestModule : AbpModule
{

}
