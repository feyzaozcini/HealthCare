using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.Appointments
{
    public class AppointmentManager(IAppointmentRepository appointmentRepository) : DomainService
    {
        public virtual async Task<Appointment> CreateAsync(
            Guid patientId, Guid doctorId, Guid departmentId, Guid appointmentTyeId,DateTime appointmentDate, TimeSpan startTime, TimeSpan endTime, String note, AppointmentStatus appointmentStatus)
        {
            Check.NotNull(patientId, nameof(patientId));
            Check.NotNull(doctorId, nameof(doctorId));
            Check.NotNull(departmentId, nameof(departmentId));
            Check.NotNull(appointmentTyeId, nameof(appointmentTyeId));
            Check.NotNull(appointmentDate, nameof(appointmentDate));
            Check.NotNull(startTime, nameof(startTime));
            Check.NotNull(endTime, nameof(endTime));
            Check.Length(note, nameof(note), AppointmentConst.NoteMaxLength);
            Check.Range((int)appointmentStatus, nameof(appointmentStatus), 1, 10);

            var appointment = new Appointment(
                GuidGenerator.Create(),
                appointmentDate,
                startTime,
                endTime,
                note,
                appointmentStatus,
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
            DateTime appointmentDate,
            TimeSpan startTime,
            TimeSpan endTime,
            String note,
            AppointmentStatus appointmentStatus
        )
        {
            var appointment = await appointmentRepository.GetAsync(id);

            appointment.SetAppointmentDate(appointmentDate);
            appointment.SetStartTime(startTime);
            appointment.SetEndTime(endTime);
            appointment.SetNote(note);
            appointment.SetAppointmentStatus(appointmentStatus);
            appointment.SetPatientId(patientId);
            appointment.SetDoctorId(doctorId);
            appointment.SetDepartmentId(departmentId);
            appointment.SetAppointmentType(appointmentTyeId);

            return await appointmentRepository.UpdateAsync(appointment);
        }

    }
}
