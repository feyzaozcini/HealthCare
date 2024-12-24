using Pusula.Training.HealthCare.Doctors;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.AppointmentTypes
{
    public interface IAppointmentTypeRepository : IRepository<AppointmentType, Guid>
    {
        
        Task<List<AppointmentType>> GetAppointmentTypesByDoctorIdsAsync(List<Guid> doctorIds);
        //randevu tipine göre doktorları getirir
        Task<List<DoctorWithNavigationProperties>> GetDoctorsByAppointmentTypeIdAsync(Guid appointmentTypeId);
        //doktorun randevu tiplerini getirir
        Task<List<AppointmentType>> GetAppointmentTypesForDoctorAsync(Guid doctorId);
        //many to many için
        Task RemoveAllDoctorsByAppointmentTypeIdAsync(Guid appointmentTypeId);

        Task DeleteAllAsync(
            string? filterText = null,
            string? name = null,
            int? durationInMinutes=null,
            CancellationToken cancellationToken = default);

        Task<List<AppointmentType>> GetListAsync(
                    string? filterText = null,
                    string? name = null,
                    int? durationInMinutes=null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(
            string? filterText = null,
            string? name = null,
            int? durationInMinutes=null,
            CancellationToken cancellationToken = default);
    }
}
