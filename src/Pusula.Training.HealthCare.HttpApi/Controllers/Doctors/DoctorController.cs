using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.Controllers.Doctors
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Doctors")]
    [Route("api/app/doctors")]

    public class DoctorController(IDoctorsAppService doctorsAppService) : HealthCareController, IDoctorsAppService
    {
        [HttpGet("{id}/with-departments")]
        public async Task<DoctorWithDepartmentDto> GetWithDepartmentsAsync(Guid id)
        {
            return await doctorsAppService.GetWithDepartmentsAsync(id);
        }
        [HttpPost]
        public Task<DoctorDto> CreateAsync(DoctorCreateDto input)
        {
          return   doctorsAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("all")]
        public Task DeleteAllAsync(GetDoctorsInput input)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("")]
        public Task DeleteByIdsAsync(List<Guid> doctorIds)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{id}")]
        public Task<DoctorDto> GetAsync(Guid id)
        {
            return doctorsAppService.GetAsync(id);
        }
        [HttpGet]
        [Route("department-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetDepartmentLookupAsync(LookupRequestDto input)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("by-user/{userId}")]
        public Task<DoctorDto> GetDoctorWithUserIdAsync(Guid userId)
        {
            return doctorsAppService.GetDoctorWithUserIdAsync(userId);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("identityUser-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetIdentityUserLookupAsync(LookupRequestDto input)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("as-excel-file")]
        public Task<IRemoteStreamContent> GetListAsExcelFileAsync(DoctorExcelDownloadDto input)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public Task<PagedResultDto<DoctorWithNavigationPropertiesDto>> GetListAsync(GetDoctorsInput input) => doctorsAppService.GetListAsync(input);

        [HttpGet]
        [Route("title-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetTitleLookupAsync(LookupRequestDto input)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<DoctorWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{id}")]
        public Task<DoctorDto> UpdateAsync(DoctorUpdateDto input)
        {
            return doctorsAppService.UpdateAsync(input);
        }
    }
}
