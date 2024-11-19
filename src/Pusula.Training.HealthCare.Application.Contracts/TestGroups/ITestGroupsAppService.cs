using Pusula.Training.HealthCare.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.TestGroups;

public interface ITestGroupsAppService : IApplicationService
{
    Task<PagedResultDto<TestGroupDto>> GetListAsync(GetTestGroupsInput input);
    Task<PagedResultDto<LookupDto<Guid>>> GetGroupNameLookupAsync(LookupRequestDto input);
    Task<TestGroupDto> GetAsync(Guid id);
    Task<TestGroupsDeletedDto> DeleteAsync(Guid id);
    Task<TestGroupDto> CreateAsync(TestGroupsCreateDto input);
    Task<TestGroupDto> UpdateAsync(TestGroupsUpdateDto input);
    Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}