using HQSOFT.eBiz.Inventor.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace HQSOFT.eBiz.Inventor;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(InventorEntityFrameworkCoreTestModule)
    )]
public class InventorDomainTestModule : AbpModule
{

}
