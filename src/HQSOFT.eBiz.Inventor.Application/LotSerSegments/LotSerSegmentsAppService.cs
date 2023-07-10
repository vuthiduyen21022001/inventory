using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using HQSOFT.eBiz.Inventor.Permissions;
using HQSOFT.eBiz.Inventor.LotSerSegments;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using HQSOFT.eBiz.Inventor.Shared;

namespace HQSOFT.eBiz.Inventor.LotSerSegments
{

    [Authorize(InventorPermissions.LotSerSegments.Default)]
    public class LotSerSegmentsAppService : ApplicationService, ILotSerSegmentsAppService
    {
        private readonly IDistributedCache<LotSerSegmentExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ILotSerSegmentRepository _lotSerSegmentRepository;
        private readonly LotSerSegmentManager _lotSerSegmentManager;

        public LotSerSegmentsAppService(ILotSerSegmentRepository lotSerSegmentRepository, LotSerSegmentManager lotSerSegmentManager, IDistributedCache<LotSerSegmentExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _lotSerSegmentRepository = lotSerSegmentRepository;
            _lotSerSegmentManager = lotSerSegmentManager;
        }

        public virtual async Task<PagedResultDto<LotSerSegmentDto>> GetListAsync(GetLotSerSegmentsInput input)
        {
            var totalCount = await _lotSerSegmentRepository.GetCountAsync(input.FilterText, input.SegmentIDMin, input.SegmentIDMax, input.AsgmentType, input.Value);
            var items = await _lotSerSegmentRepository.GetListAsync(input.FilterText, input.SegmentIDMin, input.SegmentIDMax, input.AsgmentType, input.Value, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<LotSerSegmentDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<LotSerSegment>, List<LotSerSegmentDto>>(items)
            };
        }

        public virtual async Task<LotSerSegmentDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<LotSerSegment, LotSerSegmentDto>(await _lotSerSegmentRepository.GetAsync(id));
        }

        [Authorize(InventorPermissions.LotSerSegments.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _lotSerSegmentRepository.DeleteAsync(id);
        }

        [Authorize(InventorPermissions.LotSerSegments.Create)]
        public virtual async Task<LotSerSegmentDto> CreateAsync(LotSerSegmentCreateDto input)
        {

            var lotSerSegment = await _lotSerSegmentManager.CreateAsync(
            input.SegmentID, input.AsgmentType, input.Value
            );

            return ObjectMapper.Map<LotSerSegment, LotSerSegmentDto>(lotSerSegment);
        }

        [Authorize(InventorPermissions.LotSerSegments.Edit)]
        public virtual async Task<LotSerSegmentDto> UpdateAsync(Guid id, LotSerSegmentUpdateDto input)
        {

            var lotSerSegment = await _lotSerSegmentManager.UpdateAsync(
            id,
            input.SegmentID, input.AsgmentType, input.Value, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<LotSerSegment, LotSerSegmentDto>(lotSerSegment);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(LotSerSegmentExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _lotSerSegmentRepository.GetListAsync(input.FilterText, input.SegmentIDMin, input.SegmentIDMax, input.AsgmentType, input.Value);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<LotSerSegment>, List<LotSerSegmentExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "LotSerSegments.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new LotSerSegmentExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}