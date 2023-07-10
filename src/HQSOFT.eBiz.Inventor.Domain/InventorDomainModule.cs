using Volo.Abp.Caching;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace HQSOFT.eBiz.Inventor;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AbpCachingModule),
    typeof(InventorDomainSharedModule)
)]
public class InventorDomainModule : AbpModule
{

}
