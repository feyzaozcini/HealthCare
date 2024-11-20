using Pusula.Training.HealthCare.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Districts
{
    public interface IDistrictsAppService : IApplicationService
    {
        Task<PagedResultDto<DistrictDto>> GetListAsync(GetDistrictsInput input);
        Task<DistrictDto> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<DistrictDto> CreateAsync(DistrictCreateDto input);
        Task<DistrictDto> UpdateAsync(Guid id, DistrictUpdateDto input);
        Task<IRemoteStreamContent> GetListAsExcelFileAsync(DistrictExcelDownloadDto input);
        Task DeleteByIdsAsync(List<Guid> districtIds);
        Task DeleteAllAsync(GetDistrictsInput input);
        Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
