using Pusula.Training.HealthCare.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Pusula.Training.HealthCare.AppointmentRules
{
    public interface IAppointmentRulesAppService :IApplicationService
    {
        Task<PagedResultDto<AppointmentRuleWithNavigationPropertiesDto>> GetListAsync(GetAppointmentRulesInput input);

        Task<AppointmentRuleWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<AppointmentRuleDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetDepartmentLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetDoctorLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<AppointmentRuleDto> CreateAsync(AppointmentRuleCreateDto input);

        Task<AppointmentRuleDto> UpdateAsync(AppointmentRuleUpdateDto input);

        //Departmana ait kuralları getirmek için kullanılır
        Task<List<AppointmentRuleDto>> GetRulesForDepartmentAsync(Guid departmentId);

        //Doktora ait kuralları getirmek için kullanılır
        Task<List<AppointmentRuleDto>> GetRulesForDoctorAsync(Guid doctorId);

        Task<Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}
