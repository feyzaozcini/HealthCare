using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using MiniExcelLibs;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.LabRequests;
using Pusula.Training.HealthCare.Notes;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Permissions;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Content;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Protocols
{
    [RemoteService(IsEnabled = false)]
    [Authorize(HealthCarePermissions.Protocols.Default)]
    public class ProtocolsAppService(
        IProtocolRepository protocolRepository, 
        NoteManager noteManager,
        ProtocolManager protocolManager, 
        IDistributedCache<ProtocolDownloadTokenCacheItem, string> downloadTokenCache, 
        IPatientRepository patientRepository, 
        IDepartmentRepository departmentRepository) : HealthCareAppService, IProtocolsAppService
    {


        [Authorize(HealthCarePermissions.Protocols.Create)]
        public virtual async Task<ProtocolDto> CreateAsync(ProtocolCreateDto input)
        {
            var note = await noteManager.CreateAsync(input.NoteText);

            var protocol = await protocolManager.CreateAsync(
            input.StartTime, input.EndTime, input.ProtocolStatus, input.ProtocolTypeId, note.Id,
            input.ProtocolInsuranceId, input.PatientId, input.DepartmentId, input.DoctorId
            );

            return ObjectMapper.Map<Protocol, ProtocolDto>(protocol);
        }


        [Authorize(HealthCarePermissions.Protocols.Delete)]
        public virtual async Task DeleteAsync(Guid id) => await protocolRepository.DeleteAsync(id);

        public virtual async Task<ProtocolDto> GetAsync(Guid id) => ObjectMapper.Map<Protocol, ProtocolDto>(await protocolRepository.GetAsync(id));



        public virtual async Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await downloadTokenCache.SetAsync(
                token,
                new ProtocolDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new Shared.DownloadTokenResultDto
            {
                Token = token
            };
        }


        public virtual async Task<PagedResultDto<ProtocolDto>> GetListAsync(GetProtocolsInput input)
        {
            var totalCount = await protocolRepository.GetCountAsync(
                input.FilterText,
                input.StartTime,
                input.EndTime,
                input.ProtocolStatus,
                input.ProtocolTypeId,
                input.ProtocolNoteId,
                input.ProtocolInsuranceId,
                input.PatientId,
                input.DepartmentId,
                input.DoctorId,
                input.No
                );

            var items = await protocolRepository.GetListAsync(
                input.FilterText,
                input.StartTime,
                input.EndTime,
                input.ProtocolStatus,
                input.ProtocolTypeId,
                input.ProtocolNoteId,
                input.ProtocolInsuranceId,
                input.PatientId,
                input.DepartmentId,
                input.DoctorId,
                input.No,
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount
                );
            

            return new PagedResultDto<ProtocolDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Protocol>, List<ProtocolDto>>(items)
            };
        }


        public virtual async Task<PagedResultDto<ProtocolDto>> GetListWithNavigationPropertiesAsync(GetProtocolsInput input)
        {
            var totalCount = await protocolRepository.GetCountAsync(
                input.FilterText,
                input.StartTime,
                input.EndTime,
                input.ProtocolStatus,
                input.ProtocolTypeId,
                input.ProtocolNoteId,
                input.ProtocolInsuranceId,
                input.PatientId,
                input.DepartmentId,
                input.DoctorId,
                input.No
                );
            var items = await protocolRepository.GetListWithNavigationPropertiesAsync(
                input.FilterText,
                input.StartTime,
                input.EndTime,
                input.ProtocolStatus,
                input.ProtocolTypeId,
                input.ProtocolNoteId,
                input.ProtocolInsuranceId,
                input.PatientId,
                input.DepartmentId,
                input.DoctorId,
                input.No,
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount
                );

            return new PagedResultDto<ProtocolDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Protocol>, List<ProtocolDto>>(items)
            };
        }


        public virtual async Task<ProtocolDto> GetWithNavigationPropertiesAsync(Guid id) => ObjectMapper.Map<Protocol, ProtocolDto>(await protocolRepository.GetWithNavigationPropertiesAsync(id));


        [Authorize(HealthCarePermissions.Protocols.Edit)]
        public virtual async Task<ProtocolDto> UpdateAsync(Guid id, ProtocolUpdateDto input)
        {
            var protocol = await protocolManager.UpdateAsync(
            id,
            input.StartTime, input.EndTime, input.ProtocolStatus, input.ProtocolTypeId, input.ProtocolNoteId,
            input.ProtocolInsuranceId, input.PatientId, input.DepartmentId, input.DoctorId, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Protocol, ProtocolDto>(protocol);
        }


        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetPatientLookupAsync(LookupRequestDto input)
        {
            var query = (await patientRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.FirstName != null && x.FirstName.Contains(input.Filter!));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Patients.Patient>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Patients.Patient>, List<LookupDto<Guid>>>(lookupData)
            };
        }


        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetDepartmentLookupAsync(LookupRequestDto input)
        {
            var query = (await departmentRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null && x.Name.Contains(input.Filter!));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Departments.Department>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Departments.Department>, List<LookupDto<Guid>>>(lookupData)
            };
        }


        [Authorize(HealthCarePermissions.Protocols.Delete)]
        public virtual async Task DeleteByIdsAsync(List<Guid> protocolIds) => await protocolRepository.DeleteManyAsync(protocolIds);


        [Authorize(HealthCarePermissions.Protocols.Delete)]
        public virtual async Task DeleteAllAsync(GetProtocolsInput input) => await protocolRepository.DeleteAllAsync(
            input.FilterText, input.StartTime, input.EndTime, input.ProtocolStatus, input.ProtocolTypeId, input.ProtocolNoteId,
            input.ProtocolInsuranceId, input.PatientId, input.DepartmentId, input.DoctorId, input.No);
    }
}