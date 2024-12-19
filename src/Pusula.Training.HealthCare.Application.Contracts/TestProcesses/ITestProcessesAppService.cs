using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.TestProcesses;

public interface ITestProcessesAppService : IApplicationService
{
    Task<PagedResultDto<TestProcessDto>> GetListAsync(GetTestProcessesInput input);
    Task<PagedResultDto<TestProcessWithNavigationPropertiesDto>> GetListWithNavigationPropertiesAsync(GetTestProcessesInput input);
    Task<List<TestProcessWithNavigationPropertiesDto>> GetByLabRequestIdAsync(Guid labRequestId);
    Task<List<TestCountDto>> GetTestCountsAsync();
    Task<List<TestGroupCountDto>> GetTestGroupCountsAsync();
    Task<TestProcessWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);
    Task<TestProcessDto> GetAsync(Guid id);
    Task<TestProcessesDeletedDto> DeleteAsync(Guid id);
    Task<TestProcessDto> CreateAsync(TestProcessesCreateDto input);
    Task<TestProcessDto> UpdateAsync(TestProcessesUpdateDto input);
    Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}
