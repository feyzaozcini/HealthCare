using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Pusula.Training.HealthCare.Core.Rules.Countries;
using Pusula.Training.HealthCare.Core.Rules.PatientCompanies;
using MiniExcelLibs;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Content;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.Countries;

[RemoteService(IsEnabled = false)]
[Authorize(HealthCarePermissions.Countries.Default)]
public class CountriesAppService(
    ICountryRepository countryRepository,
    CountryBusinessRules countryBusinessRules,
    CountryManager countryManager,
    IDistributedCache<CountryDownloadTokenCacheItem, string> downloadTokenCache)
    : HealthCareAppService, ICountriesAppService
    
{
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

    public virtual async Task<CountryDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<Country, CountryDto>(await countryRepository.GetAsync(id));
    }

    [Authorize(HealthCarePermissions.Countries.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        var country = await countryRepository.FindAsync(id);
        if (country == null)
        {
            throw new EntityNotFoundException(typeof(Country), id);
        }

        await countryRepository.DeleteAsync(id);
    }




    [Authorize(HealthCarePermissions.Countries.Create)]
    public virtual async Task<CountryDto> CreateAsync(CountryCreateDto input)
    {

        await countryBusinessRules.CountryNameCannotBeDuplicatedWhenInserted(input.Name);
        var country = await countryManager.CreateAsync(
            input.Name,
            input.Code
            );
        return ObjectMapper.Map<Country, CountryDto>(country);
    }

    [Authorize(HealthCarePermissions.Countries.Edit)]
    public virtual async Task<CountryDto> UpdateAsync(Guid id, CountryUpdateDto input)
    {
        var country = await countryManager.UpdateAsync(
            id,
            input.Name,
            input.Code
        );

        return ObjectMapper.Map<Country, CountryDto>(country);
    }

    [Authorize(HealthCarePermissions.Countries.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> countryIds)
    {
        await countryRepository.DeleteManyAsync(countryIds);
    }


    [Authorize(HealthCarePermissions.Countries.Delete)]
    public virtual async Task DeleteAllAsync(GetCountriesInput input)
    {
        await countryRepository.DeleteAllAsync(input.FilterText, input.Name, input.Code);
    }

    

    [AllowAnonymous]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(CountryExcelDownloadDto input)
    {
        var downloadToken = await downloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var items = await countryRepository.GetListAsync(input.FilterText, input.Name);

        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Country>, List<CountryExcelDownloadDto>>(items));
        memoryStream.Seek(0, SeekOrigin.Begin);

        return new RemoteStreamContent(memoryStream, "Countries.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
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
}
