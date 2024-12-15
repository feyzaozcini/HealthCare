using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.AppointmentRules
{
    public interface IAppointmentRuleRepository : IRepository<AppointmentRule,Guid>
    {
        //Departmana ait kuralları getirmek için kullanılır
        Task<List<AppointmentRule>> GetRulesForDepartmentAsync(Guid departmentId);

        //Doktora ait kuralları getirmek için kullanılır
        Task<List<AppointmentRule>> GetRulesForDoctorAsync(Guid doctorId);


        Task DeleteAllAsync(
            string? filterText = null,
            Guid? doctorId = null,
            Guid? departmentId = null,
            Gender? gender = null,
            int? age = null,
            int? minAge = null,
            int? maxAge = null,
            string? description = null,
            CancellationToken cancellationToken = default
        );

        Task<AppointmentRuleWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id,
            CancellationToken cancellationToken = default);

        Task<List<AppointmentRuleWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            Guid? doctorId = null,
            Guid? departmentId = null,
            Gender? gender = null,
            int? age = null,
            int? minAge = null,
            int? maxAge = null,
            string? description = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<AppointmentRule>> GetListAsync(
            string? filterText = null,
            Guid? doctorId = null,
            Guid? departmentId = null,
            Gender? gender = null,
            int? age = null,
            int? minAge = null,
            int? maxAge = null,
            string? description = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string? filterText = null,
            Guid? doctorId = null,
            Guid? departmentId = null,
            Gender? gender = null,
            int? age = null,
            int? minAge = null,
            int? maxAge = null,
            string? description = null,
            CancellationToken cancellationToken = default
        );
    }
}
