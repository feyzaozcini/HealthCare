using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Pusula.Training.HealthCare.Core.Rules.Patients;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.PatientCompanies;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Numerics;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Content;
using Volo.Abp.EventBus.Distributed;

namespace Pusula.Training.HealthCare.Patients
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Patients.Default)]
    public class PatientsAppService(IPatientRepository patientRepository, PatientManager patientManager,
        IDistributedCache<PatientDownloadTokenCacheItem, string> downloadTokenCache,
        IDistributedEventBus distributedEventBus,
        ICountryRepository countryRepository,
        IPatientCompanyRepository patientCompanyRepository,
        PatientBusinessRules patientBusinessRules
        ) : HealthCareAppService, IPatientsAppService
    {

        public virtual async Task<PagedResultDto<PatientWithNavigationPropertiesDto>> GetListAsync(GetPatientsInput input)
        {
            var totalCount = await patientRepository.GetCountAsync(input.FilterText, input.FirstName, input.LastName,input.BirthDateMin,input.BirthDateMax,input.IdentityNumber,input.PassportNumber,input.Email,input.MobilePhoneNumber,
                input.EmergencyPhoneNumber,input.Gender,input.No,input.MotherName,input.FatherName,input.BloodType,input.Type,input.CompanyId,input.CountryId, input.Sorting, input.MaxResultCount, input.SkipCount);

            var items = await patientRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.FirstName, input.LastName, input.BirthDateMin, input.BirthDateMax, input.IdentityNumber, input.PassportNumber, input.Email, input.MobilePhoneNumber,
                input.EmergencyPhoneNumber, input.Gender, input.No, input.MotherName, input.FatherName, input.BloodType, input.Type, input.CompanyId, input.CountryId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<PatientWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<PatientWithNavigationProperties>, List<PatientWithNavigationPropertiesDto>>(items)
            };
        }


        // Company ve Country lookuplarýný getirir
        public virtual async Task<PatientWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            var patient = await patientRepository.GetWithNavigationPropertiesAsync(id);
            await distributedEventBus.PublishAsync(new PatientCountryAndCompanyEto { Country = patient.Country.Name, Company = patient.PatientCompany.Name });
            return ObjectMapper.Map<PatientWithNavigationProperties, PatientWithNavigationPropertiesDto>(patient);
        }


        public virtual async Task<PatientDto> GetAsync(Guid id) => ObjectMapper.Map<Patient, PatientDto>(
                await patientRepository.GetAsync(id));

        //Company Lookup
        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input)
        {
            var query = (await patientCompanyRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null && x.Name.Contains(input.Filter!));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<PatientCompany>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<PatientCompany>, List<LookupDto<Guid>>>(lookupData)
            };
        }


        public virtual async Task<PagedResultDto<GetCountryLookupDto<Guid>>> GetCountryLookupAsync(LookupRequestDto input)
        {
            var query = (await countryRepository.GetQueryableAsync())
        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
            x => x.Name.Contains(input.Filter!) || x.Code.Contains(input.Filter!)); // Hem Name hem de Code üzerinden filtreleme

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Country>();
            var totalCount = query.Count();

            // AutoMapper ile Country'den LookupDto'ya dönüþtürme
            var lookupDtoList = ObjectMapper.Map<List<Country>, List<GetCountryLookupDto<Guid>>>(lookupData);

            return new PagedResultDto<GetCountryLookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = lookupDtoList
            };
        }

        [Authorize(HealthCarePermissions.Patients.Create)]
        public virtual async Task<PatientDto> CreateAsync(PatientCreateDto input)
        {
            var patient = await patientManager.CreateAsync(input.CompanyId, input.CountryId, input.FirstName, input.LastName, input.BirthDate, input.IdentityNumber, input.PassportNumber, input.Email, input.MobilePhoneNumber, input.EmergencyPhoneNumber, input.Gender, input.MotherName, input.FatherName, input.BloodType, input.Type);

            await patientBusinessRules.IdentityNumberCannotBeDuplicatedWhenInserted(input.IdentityNumber);

            return ObjectMapper.Map<Patient, PatientDto>(patient);
        }

        [Authorize(HealthCarePermissions.Patients.Edit)]
        public virtual async Task<PatientDto> UpdateAsync(Guid id, PatientUpdateDto input) => ObjectMapper.Map<Patient, PatientDto>(
                await patientManager.UpdateAsync(
                    input.Id,input.CompanyId,input.CountryId, input.FirstName, input.LastName, input.BirthDate, input.IdentityNumber, input.PassportNumber, input.Email, input.MobilePhoneNumber, input.EmergencyPhoneNumber, input.Gender, input.MotherName, input.FatherName, input.BloodType, input.Type));

        [Authorize(HealthCarePermissions.Patients.Delete)]
        public virtual async Task<PatientDeletedDto> DeleteAsync(Guid id)
        {
            await patientBusinessRules.PatientNotFount(id);
            Patient? patient = await patientRepository.GetAsync(predicate: c => c.Id == id);
            await patientRepository.DeleteAsync(id);
            PatientDeletedDto response = ObjectMapper.Map<Patient, PatientDeletedDto>(patient);
            response.Message = "Patient deleted successfully";
            return response;
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(PatientExcelDownloadDto input)
        {
            throw new NotImplementedException();
        }

        [Authorize(HealthCarePermissions.Patients.Delete)]
        public virtual async Task DeleteAllAsync(GetPatientsInput input) => await patientRepository.DeleteAllAsync(input.FilterText, input.FirstName, input.LastName, input.BirthDateMin, input.BirthDateMax, input.IdentityNumber, input.PassportNumber, input.Email, input.MobilePhoneNumber,
                input.EmergencyPhoneNumber, input.Gender, input.No, input.MotherName, input.FatherName, input.BloodType, input.Type, input.CompanyId, input.CountryId);

        [Authorize(HealthCarePermissions.Patients.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> patientIds) => await patientRepository.DeleteManyAsync(patientIds);


        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
                token,
                new PatientDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new Shared.DownloadTokenResultDto
            {
                Token = token
            };
        }

        
    }
}