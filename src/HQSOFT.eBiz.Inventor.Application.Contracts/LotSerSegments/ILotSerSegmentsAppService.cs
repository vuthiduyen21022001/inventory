using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using HQSOFT.eBiz.Inventor.Shared;

namespace HQSOFT.eBiz.Inventor.LotSerSegments
{
    public interface ILotSerSegmentsAppService : IApplicationService
    {
        Task<PagedResultDto<LotSerSegmentDto>> GetListAsync(GetLotSerSegmentsInput input);

        Task<LotSerSegmentDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<LotSerSegmentDto> CreateAsync(LotSerSegmentCreateDto input);

        Task<LotSerSegmentDto> UpdateAsync(Guid id, LotSerSegmentUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(LotSerSegmentExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}