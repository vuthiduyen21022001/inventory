using HQSOFT.eBiz.Inventor.LotSerSegments;
using System;
using HQSOFT.eBiz.Inventor.Shared;
using Volo.Abp.AutoMapper;
using HQSOFT.eBiz.Inventor.LotSerClasses;
using AutoMapper;

namespace HQSOFT.eBiz.Inventor;

public class InventorApplicationAutoMapperProfile : Profile
{
    public InventorApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<LotSerClass, LotSerClassDto>();
        CreateMap<LotSerClass, LotSerClassExcelDto>();

        CreateMap<LotSerSegment, LotSerSegmentDto>();
        CreateMap<LotSerSegment, LotSerSegmentExcelDto>();
    }
}