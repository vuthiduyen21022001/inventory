using HQSOFT.eBiz.Inventor.LotSerSegments;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace HQSOFT.eBiz.Inventor.LotSerSegments
{
    public class LotSerSegment : FullAuditedAggregateRoot<Guid>
    {
        public virtual int SegmentID { get; set; }

        public virtual Typeee AsgmentType { get; set; }

        [CanBeNull]
        public virtual string? Value { get; set; }

        public LotSerSegment()
        {

        }

        public LotSerSegment(Guid id, int segmentID, Typeee asgmentType, string value)
        {

            Id = id;
            SegmentID = segmentID;
            AsgmentType = asgmentType;
            Value = value;
        }

    }
}