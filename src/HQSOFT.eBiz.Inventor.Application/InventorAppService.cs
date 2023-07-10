using HQSOFT.eBiz.Inventor.Localization;
using Volo.Abp.Application.Services;

namespace HQSOFT.eBiz.Inventor;

public abstract class InventorAppService : ApplicationService
{
    protected InventorAppService()
    {
        LocalizationResource = typeof(InventorResource);
        ObjectMapperContext = typeof(InventorApplicationModule);
    }
}
