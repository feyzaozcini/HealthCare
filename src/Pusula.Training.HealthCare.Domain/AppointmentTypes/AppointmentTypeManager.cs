﻿using Pusula.Training.HealthCare.Departments;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.AppointmentTypes
{
    public class AppointmentTypeManager(IAppointmentTypeRepository appointmentTypeRepository) : DomainService
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
        int durationInMinutes
            )
        {
            var appointmentType = await appointmentTypeRepository.GetAsync(id);
            appointmentType.SetName(name);
            return await appointmentTypeRepository.UpdateAsync(appointmentType);
        }


        public virtual async Task SetDoctors(
   AppointmentType appointmentType,
   List<Guid> doctorIds
)
        {
            appointmentType.DoctorAppointmentTypes.Clear();

            foreach (var doctorId in doctorIds)
            {
                appointmentType.AddDoctor(doctorId);
            }
        }
    }
}
