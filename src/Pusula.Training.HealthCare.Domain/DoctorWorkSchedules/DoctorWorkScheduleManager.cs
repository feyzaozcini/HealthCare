using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.DoctorWorkSchedules
{
    public class DoctorWorkScheduleManager(IDoctorWorkScheduleRepository doctorWorkScheduleRepository) : DomainService
    {
        public virtual async Task<DoctorWorkSchedule> CreateAsync(
            Guid doctorId,
            int[] workingDay,
            string startHour,
            string endHour
            )
        {
            var doctorWorkSchedule = new DoctorWorkSchedule(
                GuidGenerator.Create(),
                doctorId,
                workingDay,
                startHour,
                endHour
            );

            return await doctorWorkScheduleRepository.InsertAsync(doctorWorkSchedule);
        }

        public virtual async Task<DoctorWorkSchedule> UpdateAsync(
            Guid id,
            Guid doctorId,
            int[] workingDay,
            string startHour,
            string endHour
            )
        {
            var doctorWorkSchedule = await doctorWorkScheduleRepository.GetAsync(id);

            doctorWorkSchedule.SetDoctorId(doctorId);
            doctorWorkSchedule.SetWorkingDays(workingDay);
            doctorWorkSchedule.SetStartHour(startHour);
            doctorWorkSchedule.SetEndHour(endHour);

            return await doctorWorkScheduleRepository.UpdateAsync(doctorWorkSchedule);
        }
    }
}
