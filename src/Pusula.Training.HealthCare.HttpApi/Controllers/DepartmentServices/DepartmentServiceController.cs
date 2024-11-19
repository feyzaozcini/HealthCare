using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;
using Volo.Abp;
using Pusula.Training.HealthCare.DepartmentServices;

namespace Pusula.Training.HealthCare.Controllers.DepartmentServices
{
    [RemoteService]
    [Area("app")]
    [ControllerName("DepartmentService")]
    [Route("api/app/departmentServices")]
    public class DepartmentServiceController(IDepartmentServicesAppService departmentServicesAppService)
    : HealthCareController, IDepartmentServicesAppService
    {
        [HttpGet]
        public virtual Task<PagedResultDto<DepartmentServiceDto>> GetListAsync(GetDepartmentServicesInput input) => departmentServicesAppService.GetListAsync(input);

        [HttpGet]
        [Route("{id}")]
        public virtual Task<DepartmentServiceDto> GetAsync(Guid id) => departmentServicesAppService.GetAsync(id);

        [HttpPost]
        public virtual Task<DepartmentServiceDto> CreateAsync(DepartmentServiceCreateDto input) => departmentServicesAppService.CreateAsync(input);

        [HttpPut]
        [Route("{id}")]
        public virtual Task<DepartmentServiceDto> UpdateAsync(Guid id, DepartmentServiceUpdateDto input) => departmentServicesAppService.UpdateAsync(id, input);

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id) => departmentServicesAppService.DeleteAsync(id);

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(DepartmentServiceExcelDownloadDto input) => departmentServicesAppService.GetListAsExcelFileAsync(input);

        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync() => departmentServicesAppService.GetDownloadTokenAsync();

        [HttpDelete]
        [Route("")]
        public virtual Task DeleteByIdsAsync(List<Guid> departmentServiceIds) => departmentServicesAppService.DeleteByIdsAsync(departmentServiceIds);

        [HttpDelete]
        [Route("all")]
        public virtual Task DeleteAllAsync(GetDepartmentServicesInput input) => departmentServicesAppService.DeleteAllAsync(input);
    }
}