using Pusula.Training.HealthCare.Districts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Villages
{
    public interface IVillagesAppService : IApplicationService
    {
        Task<PagedResultDto<VillageDto>> GetListAsync(GetVillagesInput input);
        Task<VillageDto> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<VillageDto> CreateAsync(VillageCreateDto input);
        Task<VillageDto> UpdateAsync(Guid id, VillageUpdateDto input);
        Task<IRemoteStreamContent> GetListAsExcelFileAsync(VillageExcelDownloadDto input);
        Task DeleteByIdsAsync(List<Guid> villageIds);
        Task DeleteAllAsync(GetVillagesInput input);
        Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
