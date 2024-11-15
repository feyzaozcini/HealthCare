using Pusula.Training.HealthCare.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Pusula.Training.HealthCare.DepartmentServices
{
    public interface IDepartmentServicesAppService : IApplicationService
    {
        Task<PagedResultDto<DepartmentServiceDto>> GetListAsync(GetDepartmentServicesInput input);

        Task<DepartmentServiceDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<DepartmentServiceDto> CreateAsync(DepartmentServiceCreateDto input);

        Task<DepartmentServiceDto> UpdateAsync(Guid id, DepartmentServiceUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(DepartmentServiceExcelDownloadDto input);
        Task DeleteByIdsAsync(List<Guid> departmentServiceIds);

        Task DeleteAllAsync(GetDepartmentServicesInput input);
        Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();

    }
}
