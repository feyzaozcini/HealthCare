using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.TestGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.TestProcesses;

public interface ITestProcessesAppService : IApplicationService
{
    Task<PagedResultDto<TestProcessDto>> GetListAsync(GetTestProcessesInput input);
    Task<TestProcessDto> GetAsync(Guid id);
    Task<TestProcessesDeletedDto> DeleteAsync(Guid id);
    Task<TestProcessDto> CreateAsync(TestProcessesCreateDto input);
    Task<TestProcessDto> UpdateAsync(TestProcessesUpdateDto input);
    Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}
