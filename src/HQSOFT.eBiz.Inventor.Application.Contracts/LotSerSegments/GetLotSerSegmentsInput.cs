using HQSOFT.eBiz.Inventor.LotSerSegments;
using Volo.Abp.Application.Dtos;
using System;

namespace HQSOFT.eBiz.Inventor.LotSerSegments
{
    public class GetLotSerSegmentsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public int? SegmentIDMin { get; set; }
        public int? SegmentIDMax { get; set; }
        public Typeee? AsgmentType { get; set; }
        public string? Value { get; set; }

        public GetLotSerSegmentsInput()
        {

        }
    }
}