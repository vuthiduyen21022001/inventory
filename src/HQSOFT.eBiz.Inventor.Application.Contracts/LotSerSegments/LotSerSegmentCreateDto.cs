using HQSOFT.eBiz.Inventor.LotSerSegments;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HQSOFT.eBiz.Inventor.LotSerSegments
{
    public class LotSerSegmentCreateDto
    {
        public int SegmentID { get; set; }
        public Typeee AsgmentType { get; set; } = ((Typeee[])Enum.GetValues(typeof(Typeee)))[0];
        public string? Value { get; set; }
    }
}