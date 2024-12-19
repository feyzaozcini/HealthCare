using System;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.TestProcesses
{
    public class GetTestProcessesInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }
        public Guid? LabRequestId { get; set; }
        public Guid? TestGroupItemId { get; set; }
        public TestProcessStates? Status { get; set; }
        public decimal? Result { get; set; }
        public DateTime? ResultDate { get; set; }

        // Doktor
        public string? DoctorName { get; set; }
        public string? DoctorSurname { get; set; }

        // Hasta
        public string? PatientName { get; set; }
        public string? PatientSurname { get; set; }
        public int? ProtocolNo { get; set; }

        // Test
        public string? TestGroupItemName { get; set; }

        // LabRequest
        public DateTime? LabRequestCreatedTime { get; set; }

        public GetTestProcessesInput()
        {
            MaxResultCount = 100;
        }
    }
}
