using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using HQSOFT.eBiz.Inventor.LotSerSegments;
using Volo.Abp.Content;
using HQSOFT.eBiz.Inventor.Shared;

namespace HQSOFT.eBiz.Inventor.LotSerSegments
{
    [RemoteService(Name = "Inventor")]
    [Area("inventor")]
    [ControllerName("LotSerSegment")]
    [Route("api/inventor/lot-ser-segments")]
    public class LotSerSegmentController : AbpController, ILotSerSegmentsAppService
    {
        private readonly ILotSerSegmentsAppService _lotSerSegmentsAppService;

        public LotSerSegmentController(ILotSerSegmentsAppService lotSerSegmentsAppService)
        {
            _lotSerSegmentsAppService = lotSerSegmentsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<LotSerSegmentDto>> GetListAsync(GetLotSerSegmentsInput input)
        {
            return _lotSerSegmentsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<LotSerSegmentDto> GetAsync(Guid id)
        {
            return _lotSerSegmentsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<LotSerSegmentDto> CreateAsync(LotSerSegmentCreateDto input)
        {
            return _lotSerSegmentsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<LotSerSegmentDto> UpdateAsync(Guid id, LotSerSegmentUpdateDto input)
        {
            return _lotSerSegmentsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _lotSerSegmentsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(LotSerSegmentExcelDownloadDto input)
        {
            return _lotSerSegmentsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _lotSerSegmentsAppService.GetDownloadTokenAsync();
        }
    }
}