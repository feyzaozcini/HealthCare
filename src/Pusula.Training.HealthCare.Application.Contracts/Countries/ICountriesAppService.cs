﻿using Pusula.Training.HealthCare.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.Countries;

public interface ICountriesAppService : IApplicationService
{
    Task<PagedResultDto<CountryDto>> GetListAsync(GetCountriesInput input);
    Task<CountryDto> GetAsync(Guid id);
    Task DeleteAsync(Guid id);
    Task<CountryDto> CreateAsync(CountryCreateDto input);
    Task<CountryDto> UpdateAsync(CountryUpdateDto input);
    Task DeleteByIdsAsync(List<Guid> countryIds);
    Task DeleteAllAsync(GetCountriesInput input);
    Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();

}
