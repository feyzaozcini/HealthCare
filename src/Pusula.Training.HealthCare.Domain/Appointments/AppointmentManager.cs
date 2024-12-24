using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Appointments
{
    public class AppointmentManager(IAppointmentRepository appointmentRepository) : DomainService
    {
        public virtual async Task<Appointment> CreateAsync(
            Guid patientId, Guid doctorId, Guid departmentId, Guid appointmentTyeId, DateTime startDate, DateTime endDate, String note, AppointmentStatus appointmentStatus, bool isBlok)
        {
            Check.NotNull(patientId, nameof(patientId));
            Check.NotNull(doctorId, nameof(doctorId));
            Check.NotNull(departmentId, nameof(departmentId));
            Check.NotNull(appointmentTyeId, nameof(appointmentTyeId));
            Check.NotNull(startDate, nameof(startDate));
            Check.NotNull(endDate, nameof(endDate));

            var appointment = new Appointment(
                GuidGenerator.Create(),
                startDate,
                endDate,
                note,
                appointmentStatus,
                isBlok,
                patientId,
                doctorId,
                departmentId,
                appointmentTyeId
            );

            return await appointmentRepository.InsertAsync(appointment);
        }

        public virtual async Task<Appointment> UpdateAsync(
            Guid id,
            Guid patientId,
            Guid doctorId,
            Guid departmentId,
            Guid appointmentTyeId,
            DateTime startDate,
            DateTime endDate,
            String note,
            AppointmentStatus appointmentStatus,
            bool isBlok
        )
        {
            var appointment = await appointmentRepository.GetAsync(id);
            appointment.SetStartDate(startDate);
            appointment.SetEndDate(endDate);
            appointment.SetNote(note);
            appointment.SetAppointmentStatus(appointmentStatus);
            appointment.SetBlock(isBlok);
            appointment.SetPatientId(patientId);
            appointment.SetDoctorId(doctorId);
            appointment.SetDepartmentId(departmentId);
            appointment.SetAppointmentType(appointmentTyeId);

            return await appointmentRepository.UpdateAsync(appointment);
        }

    }
}
