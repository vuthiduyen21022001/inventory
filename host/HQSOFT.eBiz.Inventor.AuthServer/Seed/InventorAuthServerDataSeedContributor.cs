using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace HQSOFT.eBiz.Inventor.Seed;

public class InventorAuthServerDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly InventorSampleIdentityDataSeeder _inventorSampleIdentityDataSeeder;
    private readonly InventorAuthServerDataSeeder _inventorAuthServerDataSeeder;
    private readonly ICurrentTenant _currentTenant;

    public InventorAuthServerDataSeedContributor(
        InventorAuthServerDataSeeder inventorAuthServerDataSeeder,
        InventorSampleIdentityDataSeeder inventorSampleIdentityDataSeeder,
        ICurrentTenant currentTenant)
    {
        _inventorAuthServerDataSeeder = inventorAuthServerDataSeeder;
        _inventorSampleIdentityDataSeeder = inventorSampleIdentityDataSeeder;
        _currentTenant = currentTenant;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            await _inventorSampleIdentityDataSeeder.SeedAsync(context!);
            await _inventorAuthServerDataSeeder.SeedAsync(context!);
        }
    }
}
