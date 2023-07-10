using HQSOFT.eBiz.Inventor.LotSerSegments;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HQSOFT.eBiz.Inventor.LotSerSegments
{
    public interface ILotSerSegmentRepository : IRepository<LotSerSegment, Guid>
    {
        Task<List<LotSerSegment>> GetListAsync(
            string filterText = null,
            int? segmentIDMin = null,
            int? segmentIDMax = null,
            Typeee? asgmentType = null,
            string value = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            int? segmentIDMin = null,
            int? segmentIDMax = null,
            Typeee? asgmentType = null,
            string value = null,
            CancellationToken cancellationToken = default);
    }
}