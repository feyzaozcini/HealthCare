using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.LabRequests;

public interface ILabRequestsAppService : IApplicationService
{
    Task<PagedResultDto<LabRequestDto>> GetListAsync(GetLabRequestsInput input);
    Task<PagedResultDto<LabRequestDto>> GetListWithNavigationPropertiesAsync(GetLabRequestsInput input);
    Task<LabRequestDto> GetWithNavigationPropertiesAsync(Guid id);
    Task<LabRequestDto> GetAsync(Guid id);
    Task<LabRequestDeletedDto> DeleteAsync(Guid id);
    Task<LabRequestDto> CreateAsync(LabRequestCreateDto input);
    Task<LabRequestDto> UpdateAsync(LabRequestUpdateDto input);
    Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
    Task NotifyTestResultsAsync(LabRequestDto labRequest);

}
