using HQSOFT.eBiz.Inventor.LotSerSegments;
using Volo.Abp.AutoMapper;
using HQSOFT.eBiz.Inventor.LotSerClasses;
using AutoMapper;

namespace HQSOFT.eBiz.Inventor.Blazor;

public class InventorBlazorAutoMapperProfile : Profile
{
    public InventorBlazorAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<LotSerClassDto, LotSerClassUpdateDto>();

        CreateMap<LotSerSegmentDto, LotSerSegmentUpdateDto>();
    }
}