﻿using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.TestProcesses;

public interface ITestProcessesAppService : IApplicationService
{
    Task<PagedResultDto<TestProcessDto>> GetListAsync(GetTestProcessesInput input);
    Task<PagedResultDto<TestProcessDto>> GetListWithNavigationPropertiesAsync(GetTestProcessesInput input);
    Task<List<TestProcessDto>> GetByLabRequestIdAsync(Guid labRequestId);
    Task<List<TestCountDto>> GetTestCountsAsync();
    Task<List<TestGroupCountDto>> GetTestGroupCountsAsync();
    Task<TestProcessDto> GetWithNavigationPropertiesAsync(Guid id);
    Task<TestProcessDto> GetAsync(Guid id);
    Task<TestProcessesDeletedDto> DeleteAsync(Guid id);
    Task<TestProcessDto> CreateAsync(TestProcessesCreateDto input);
    Task<TestProcessDto> UpdateAsync(TestProcessesUpdateDto input);
    Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}
