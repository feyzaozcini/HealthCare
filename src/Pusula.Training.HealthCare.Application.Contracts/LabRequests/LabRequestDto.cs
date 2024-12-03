using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.LabRequests;

public class LabRequestDto : FullAuditedEntity<Guid>
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

    public LabRequestDto()
    {
    }
}
