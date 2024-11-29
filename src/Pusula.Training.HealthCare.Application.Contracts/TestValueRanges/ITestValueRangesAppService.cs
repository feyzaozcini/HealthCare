using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.TestValueRanges;

public interface ITestValueRangesAppService : IApplicationService
{
    Task<PagedResultDto<TestValueRangeDto>> GetListAsync(GetTestValueRangesInput input);
    Task<PagedResultDto<TestValueRangeDto>> GetListWithNavigationPropertiesAsync(GetTestValueRangesInput input);
    Task<TestValueRangeDto> GetWithNavigationPropertiesAsync(Guid id);
    Task<TestValueRangeDto> GetAsync(Guid id);
    Task<TestValueRangesDeletedDto> DeleteAsync(Guid id);
    Task<TestValueRangeDto> CreateAsync(TestValueRangesCreateDto input);
    Task<TestValueRangeDto> UpdateAsync(TestValueRangesUpdateDto input);
    Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}
