using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Doctors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.AppointmentTypes
{
    public class AppointmentTypeManager(IAppointmentTypeRepository appointmentTypeRepository
        ) : DomainService
    {
        public virtual async Task<AppointmentType> CreateAsync(
        string name,
        int durationInMinutes,
        List<Guid> doctorIds
        )
        {
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), AppointmentTypeConst.NameMaxLength, AppointmentTypeConst.NameMinLength);
            

            var appointmentType = new AppointmentType(
             GuidGenerator.Create(),
            name,
            durationInMinutes
            );
            await SetDoctors(appointmentType, doctorIds);
            return await appointmentTypeRepository.InsertAsync(appointmentType);
        }

        public virtual async Task<AppointmentType> UpdateAsync(
            Guid id,
        string name,
        int durationInMinutes,
        List<Guid> doctorIds
            )
        {
            var appointmentType = await appointmentTypeRepository.GetAsync(id);
            appointmentType.SetName(name);
            appointmentType.SetDurationInMinutes(durationInMinutes);
            await SetDoctors(appointmentType, doctorIds);
            return await appointmentTypeRepository.UpdateAsync(appointmentType);
        }

        public virtual async Task SetDoctors(AppointmentType appointmentType,List<Guid> doctorIds)
        {
            appointmentType.DoctorAppointmentTypes.Clear();
            if(doctorIds != null) 
            {
                foreach (var doctorId in doctorIds)
                {
                    appointmentType.AddDoctor(doctorId);
                }
            }
            
        }
    }
}

