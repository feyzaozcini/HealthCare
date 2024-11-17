using Pusula.Training.HealthCare.Countries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.LabRequests;

public interface ILabRequestsAppService : IApplicationService
{
    Task<PagedResultDto<LabRequestDto>> GetListAsync(GetLabRequestsInput input);
    Task<LabRequestDto> GetAsync(Guid id);
    Task<LabRequestDeletedDto> DeleteAsync(Guid id);
    Task<LabRequestDto> CreateAsync(LabRequestCreateDto input);
    Task<LabRequestDto> UpdateAsync(LabRequestUpdateDto input);
    Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}
