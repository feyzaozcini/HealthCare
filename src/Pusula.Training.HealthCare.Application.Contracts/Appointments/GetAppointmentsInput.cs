using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Appointments
{
    public class GetAppointmentsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }
        public DateTime? AppointmentDateMin { get; set; }
        public DateTime? AppointmentDateMax { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string? Note { get; set; }
        public AppointmentStatus? AppointmentStatus { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? DoctorId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? AppointmentTypeId { get; set; }

        public GetAppointmentsInput()
        {
            
        }
    }
}
