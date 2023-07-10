using HQSOFT.eBiz.Inventor.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace HQSOFT.eBiz.Inventor;

public abstract class InventorController : AbpControllerBase
{
    protected InventorController()
    {
        LocalizationResource = typeof(InventorResource);
    }
}
