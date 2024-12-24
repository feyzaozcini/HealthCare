using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.DoctorWorkSchedules
{
    public class GetDoctorWorkSchedulesInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }
        public Guid? DoctorId { get; set; }
        public int[]? WorkingDays { get; set; }
        public string? StartHour { get; set; }
        public string? EndHour { get; set; }

        public GetDoctorWorkSchedulesInput()
        {

        }
    }
}
