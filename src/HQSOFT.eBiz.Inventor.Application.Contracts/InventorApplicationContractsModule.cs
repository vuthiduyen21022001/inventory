using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace HQSOFT.eBiz.Inventor;

[DependsOn(
    typeof(InventorDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationAbstractionsModule)
    )]
public class InventorApplicationContractsModule : AbpModule
{

}
