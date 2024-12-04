using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Appointments
{
    public interface IAppointmentRepository : IRepository<Appointment, Guid>
    {
        Task DeleteAllAsync(
            string? filterText = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? note = null,
            AppointmentStatus? appointmentStatus = null,
            bool isBlock=false,
            Guid? patientId = null,
            Guid? doctorId = null,
            Guid? departmentId = null,
            Guid? appointmentTypeId = null,
            CancellationToken cancellationToken = default);

        Task<AppointmentWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id,
            CancellationToken cancellationToken = default);

        Task<List<AppointmentWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? note = null,
            AppointmentStatus? appointmentStatus = null,
            bool? isBlock = null,
            Guid? patientId = null,
            Guid? doctorId = null,
            Guid? departmentId = null,
            Guid? appointmentTypeId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
           CancellationToken cancellationToken = default
         );

        Task<List<Appointment>> GetListAsync(
            string? filterText = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? note = null,
            AppointmentStatus? appointmentStatus = null,
            bool? isBlock = null,
            Guid? patientId = null,
            Guid? doctorId = null,
            Guid? departmentId = null,
            Guid? appointmentTypeId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
            );

        Task<long> GetCountAsync(
            string? filterText = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? note = null,
            AppointmentStatus? appointmentStatus = null,
            bool? isBlock = null ,
            Guid? patientId = null,
            Guid? doctorId = null,
            Guid? departmentId = null,
            Guid? appointmentTypeId = null,
            CancellationToken cancellationToken = default);

    }
}
