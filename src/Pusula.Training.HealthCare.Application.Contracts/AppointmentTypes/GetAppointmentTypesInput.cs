using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.AppointmentTypes
{
    public class GetAppointmentTypesInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? Name { get; set; }

        public int? DurationInMinutes { get; set; }

        public List<Guid>? DoctorAppointmentTypes { get; set; }

        public GetAppointmentTypesInput()
        {
        }
    }
}
