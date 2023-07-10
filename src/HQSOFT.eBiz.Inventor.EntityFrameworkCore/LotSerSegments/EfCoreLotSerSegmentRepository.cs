using HQSOFT.eBiz.Inventor.LotSerSegments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using HQSOFT.eBiz.Inventor.EntityFrameworkCore;

namespace HQSOFT.eBiz.Inventor.LotSerSegments
{
    public class EfCoreLotSerSegmentRepository : EfCoreRepository<InventorDbContext, LotSerSegment, Guid>, ILotSerSegmentRepository
    {
        public EfCoreLotSerSegmentRepository(IDbContextProvider<InventorDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<LotSerSegment>> GetListAsync(
            string filterText = null,
            int? segmentIDMin = null,
            int? segmentIDMax = null,
            Typeee? asgmentType = null,
            string value = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, segmentIDMin, segmentIDMax, asgmentType, value);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? LotSerSegmentConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            int? segmentIDMin = null,
            int? segmentIDMax = null,
            Typeee? asgmentType = null,
            string value = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, segmentIDMin, segmentIDMax, asgmentType, value);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<LotSerSegment> ApplyFilter(
            IQueryable<LotSerSegment> query,
            string filterText,
            int? segmentIDMin = null,
            int? segmentIDMax = null,
            Typeee? asgmentType = null,
            string value = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Value.Contains(filterText))
                    .WhereIf(segmentIDMin.HasValue, e => e.SegmentID >= segmentIDMin.Value)
                    .WhereIf(segmentIDMax.HasValue, e => e.SegmentID <= segmentIDMax.Value)
                    .WhereIf(asgmentType.HasValue, e => e.AsgmentType == asgmentType)
                    .WhereIf(!string.IsNullOrWhiteSpace(value), e => e.Value.Contains(value));
        }
    }
}