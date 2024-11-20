﻿using Pusula.Training.HealthCare.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Countries;

public interface ICountriesAppService : IApplicationService
{
    Task<PagedResultDto<CountryDto>> GetListAsync(GetCountriesInput input);
    Task<CountryDto> GetAsync(Guid id);
    Task DeleteAsync(Guid id);
    Task<CountryDto> CreateAsync(CountryCreateDto input);
    Task<CountryDto> UpdateAsync(Guid id, CountryUpdateDto input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(CountryExcelDownloadDto input); // Excel desteği eklendi
    Task DeleteByIdsAsync(List<Guid> countryIds); // Çoklu silme eklendi
    Task DeleteAllAsync(GetCountriesInput input); // Filtreye göre toplu silme eklendi
    Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}