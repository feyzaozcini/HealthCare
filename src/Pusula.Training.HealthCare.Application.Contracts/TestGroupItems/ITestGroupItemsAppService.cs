﻿using Pusula.Training.HealthCare.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using static Pusula.Training.HealthCare.Permissions.HealthCarePermissions;

namespace Pusula.Training.HealthCare.TestGroupItems;

public interface ITestGroupItemsAppService : IApplicationService
{
    Task<PagedResultDto<TestGroupItemDto>> GetListAsync(GetTestGroupItemsInput input);
    Task<TestGroupItemDto> GetWithNavigationPropertiesAsync(Guid id);
    Task<TestGroupItemDto> GetAsync(Guid id);
    Task<TestGroupItemsDeletedDto> DeleteAsync(Guid id);
    Task<TestGroupItemDto> CreateAsync(TestGroupItemsCreateDto input);
    Task<TestGroupItemDto> UpdateAsync(TestGroupItemsUpdateDto input);
    Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}
