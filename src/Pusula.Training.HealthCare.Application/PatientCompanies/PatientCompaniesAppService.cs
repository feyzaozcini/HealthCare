using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using MiniExcelLibs;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Content;
using Volo.Abp.ObjectMapping;

namespace Pusula.Training.HealthCare.PatientCompanies
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.PatientCompanies.Default)]
    public class PatientCompaniesAppService(IPatientCompanyRepository patientCompanyRepository,
        PatientCompanyManager patientCompanyManager, 
        IDistributedCache<PatientCompanyDownloadTokenCacheItem, string> downloadTokenCache,
        PatientCompanyBusinessRules patientCompanyBusinessRules)
        : HealthCareAppService, IPatientCompaniesAppService
    {
        public async Task<PagedResultDto<PatientCompanyDto>> GetListAsync(GetPatientCompaniesInput input)
        {
            var totalCount = await patientCompanyRepository.GetCountAsync(input.FilterText, input.Name);
            var items = await patientCompanyRepository.GetListAsync(input.FilterText, input.Name, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<PatientCompanyDto>
            {
                TotalCount=totalCount,
                Items=ObjectMapper.Map<List<PatientCompany>,List<PatientCompanyDto>>(items)
            };
        }
        public async Task<PatientCompanyDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<PatientCompany, PatientCompanyDto>(await patientCompanyRepository.GetAsync(id));
        }

        [Authorize(HealthCarePermissions.PatientCompanies.Delete)]
        public async Task<PatientCompanyDeleteDto> DeleteAsync(Guid id)
        {

            var patientCompany = await patientCompanyBusinessRules.PatientCompanyNotFound(id);
            //PatientCompany? patientCompany = await patientCompanyRepository.GetAsync(predicate: c => c.Id == id);

            await patientCompanyRepository.DeleteAsync(id);

            PatientCompanyDeleteDto response = ObjectMapper.Map<PatientCompany, PatientCompanyDeleteDto>(patientCompany);
            response.Message = PatientCompanyConsts.PatientCompanyDeleteMessage;

            return response;
           
        }

        [Authorize(HealthCarePermissions.PatientCompanies.Create)]
        public async Task<PatientCompanyDto> CreateAsync(PatientCompanyCreateDto input)
        {

            await patientCompanyBusinessRules.DuplicatedPatientCompanyName(input.Name);
            var patientCompany = await patientCompanyManager.CreateAsync(input.Name);
            return ObjectMapper.Map<PatientCompany, PatientCompanyDto>(patientCompany);
        }

        [Authorize(HealthCarePermissions.PatientCompanies.Edit)]
        public async Task<PatientCompanyDto> UpdateAsync(PatientCompanyUpdateDto input)
        {
            var patientCompany = await patientCompanyManager.UpdateAsync(input.Id, input.Name,input.ConcurrencyStamp);

            return ObjectMapper.Map<PatientCompany, PatientCompanyDto>(patientCompany);
        }

        [AllowAnonymous]
        public async Task<IRemoteStreamContent> GetListAsExcelFileAsync(PatientCompanyExcelDownloadDto input)
        {
            var downloadToken = await downloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await patientCompanyRepository.GetListAsync(input.FilterText, input.Name);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<PatientCompany>, List<PatientCompanyExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Companies.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [Authorize(HealthCarePermissions.PatientCompanies.Delete)]

        public async Task DeleteByIdAsync(List<Guid> patientCompanyIds)
        {
            await patientCompanyRepository.DeleteManyAsync(patientCompanyIds);
        }


        [Authorize(HealthCarePermissions.PatientCompanies.Delete)]

        public async Task DeleteAllAsync(GetPatientCompaniesInput input)
        {
            await patientCompanyRepository.DeleteAllAsync(input.FilterText, input.Name);
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
                token,
                new PatientCompanyDownloadTokenCacheItem { Token = token },
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
}
