using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.AppointmentTypes
{
    public class AppointmentTypeDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; } = null!;

        public int DurationInMinutes { get; set; }

        public List<Guid> DoctorAppointmentTypes { get; set; } = new();
    }
}
