using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.LabRequests;

public class LabRequestWithNavigationPropertiesDto
{
    public Guid Id { get; set; }
    //Protocol
    [Required]
    public Guid ProtocolId { get; set; }
    public string ProtocolType { get; set; }
    public DateTime? ProtocolStartDate { get; set; }
    public string? ProtocolEndDate { get; set; }

    //Patient

    //public string PatientName { get; set; }
    //public string PatientSurname { get; set; }
    //public int PatientNo { get; set; }

    //Doctor
    [Required]
    public Guid DoctorId { get; set; }
    public string? DoctorName { get; set; }
    public string? DoctorSurname { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public RequestStatusEnum Status { get; set; }
    [StringLength(LabRequestConsts.DescriptionMaxLength)]
    public string? Description { get; set; }

    public LabRequestWithNavigationPropertiesDto()
    {
    }
}
