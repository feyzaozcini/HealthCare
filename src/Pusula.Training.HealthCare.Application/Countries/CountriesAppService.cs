﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using static Pusula.Training.HealthCare.Permissions.HealthCarePermissions;

namespace Pusula.Training.HealthCare.Countries;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.Countries.Default)]
public class CountriesAppService(
    ICountryRepository countryRepository,
    CountryManager countryManager,
    IDistributedCache<CountryDownloadTokenCacheItem, string> downloadTokenCache)
    : HealthCareAppService, ICountriesAppService
    
{
    [Authorize(HealthCarePermissions.Countries.Create)]
    public virtual async Task<CountryDto> CreateAsync(CountryCreateDto input)
    {
        var country = await countryManager.CreateAsync(
            input.Name,
            input.Code
            );

        return ObjectMapper.Map<Country, CountryDto>(country);
    }

    [Authorize(HealthCarePermissions.Countries.Delete)]
    public virtual async Task DeleteAllAsync(GetCountriesInput input)
    {
        await countryRepository.DeleteAllAsync(input.FilterText, input.Name, input.Code);
    }

    [Authorize(HealthCarePermissions.Countries.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await countryRepository.DeleteAsync(id);
    }

    [Authorize(HealthCarePermissions.Countries.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> countryIds)
    {
        await countryRepository.DeleteManyAsync(countryIds);
    }


    public virtual async Task<CountryDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<Country, CountryDto>(await countryRepository.GetAsync(id));
    }

    public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");

        await downloadTokenCache.SetAsync(
            token,
            new CountryDownloadTokenCacheItem { Token = token },
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
            });

        return new DownloadTokenResultDto
        {
            Token = token
        };
    }

    public virtual async Task<PagedResultDto<CountryDto>> GetListAsync(GetCountriesInput input)
    {
        var totalCount = await countryRepository.GetCountAsync(input.FilterText, input.Name, input.Code);
        var items = await countryRepository.GetListAsync(input.FilterText, input.Name, input.Code, input.Sorting, input.MaxResultCount, input.SkipCount);

        return new PagedResultDto<CountryDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<Country>, List<CountryDto>>(items)
        };
    }

    [Authorize(HealthCarePermissions.Countries.Edit)]
    public virtual async Task<CountryDto> UpdateAsync(CountryUpdateDto input)
    {
        var country = await countryManager.UpdateAsync(
                    input.Id,
                    input.Name, 
                    input.Code
                    );

        return ObjectMapper.Map<Country, CountryDto>(country);
    }
}
