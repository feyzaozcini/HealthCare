using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.TestProcesses;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.LabRequests;

public class LabRequestDto : FullAuditedEntity<Guid>
{
    //Protocol
    [Required]
    public Guid ProtocolId { get; set; }
    public string? ProtocolType { get; set; }
    public DateTime? ProtocolStartDate { get; set; }
    public string? ProtocolEndDate { get; set; }
    public int ProtocolNo { get; set; }

    //Insurance
    public string? InsuranceName { get; set; }

    //Patient
    public PatientDto Patient { get; set; }
    public TestProcessesCreateDto TestProcess { get; set; }


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
