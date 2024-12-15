using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.DoctorWorkSchedules
{
    public interface IDoctorWorkScheduleRepository : IRepository<DoctorWorkSchedule, Guid>
    {
        Task<List<DoctorWorkSchedule>> GetWorkScheduleForDoctorAsync(Guid doctorId);

        Task DeleteAllAsync(
               string? filterText = null,
               Guid? doctorId = null,
               int[]? workingDays = null,
               string? startHour = null,
               string? endHour = null,
               CancellationToken cancellationToken = default
               );

        Task<List<DoctorWorkSchedule>> GetListAsync(
                    string? filterText = null,
                    Guid? doctorId = null,
                    int[]? workingDays = null,
                    string? startHour = null,
                    string? endHour = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                    );

        Task<long> GetCountAsync(
                    string? filterText = null,
                    Guid? doctorId = null,
                    int[]? workingDays = null,
                    string? startHour = null,
                    string? endHour = null,
                    CancellationToken cancellationToken = default
                    );

        Task<DoctorWorkScheduleWithNavigationProperties> GetWithNavigationPropertiesAsync(
                    Guid id,
                    CancellationToken cancellationToken = default
                    );

        Task<List<DoctorWorkScheduleWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
                    string? filterText = null,
                    Guid? doctorId = null,
                    int[]? workingDays = null,
                    string? startHour = null,
                    string? endHour = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                    );
    }
}
