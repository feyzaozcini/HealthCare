using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.AppointmentTypes
{
    public class AppointmentTypeDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; } = null!;
    }
}
