using HQSOFT.eBiz.Inventor.Localization;
using Volo.Abp.AspNetCore.Components;

namespace HQSOFT.eBiz.Inventor.Blazor;

public abstract class InventorComponentBase : AbpComponentBase
{
    protected InventorComponentBase()
    {
        LocalizationResource = typeof(InventorResource);
    }
}
