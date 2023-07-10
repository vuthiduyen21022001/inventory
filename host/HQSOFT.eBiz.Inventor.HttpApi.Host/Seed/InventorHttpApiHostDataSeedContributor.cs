using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace HQSOFT.eBiz.Inventor.Seed;

public class InventorHttpApiHostDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly InventorSampleDataSeeder _inventorSampleDataSeeder;
    private readonly ICurrentTenant _currentTenant;

    public InventorHttpApiHostDataSeedContributor(
        InventorSampleDataSeeder inventorSampleDataSeeder,
        ICurrentTenant currentTenant)
    {
        _inventorSampleDataSeeder = inventorSampleDataSeeder;
        _currentTenant = currentTenant;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            await _inventorSampleDataSeeder.SeedAsync(context!);
        }
    }
}
