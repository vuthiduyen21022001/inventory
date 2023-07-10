using HQSOFT.eBiz.Inventor.LotSerSegments;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace HQSOFT.eBiz.Inventor.LotSerSegments
{
    public class LotSerSegmentDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public int SegmentID { get; set; }
        public Typeee AsgmentType { get; set; }
        public string? Value { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}