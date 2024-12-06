﻿using JetBrains.Annotations;
using Pusula.Training.HealthCare.TestValueRanges;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.TestProcesses;

public class TestProcessDto : FullAuditedEntityDto<Guid>
{
    [Required]
    public Guid LabRequestId { get; set; }
    public DateTime LabRequestDate { get; set; }
    public string? DoctorName { get; set; }
    public string? DoctorSurname { get; set; }
    public string PatientName { get; set; } = null!;
    public string PatientSurname { get; set; } = null!;
    [Required]
    public Guid TestGroupItemId { get; set; }
    public string TestGroupItemName { get; set; } = null!;
    public string TestGroupName { get; set; } = null!;

    public decimal TestMinValue { get; set; }
    public decimal TestMaxValue { get; set; }
    public TestUnitTypes TestUnit { get; set; }
    [Required]
    public TestProcessStates Status { get; set; }
    public decimal? Result { get; set; }
    public DateTime? ResultDate { get; set; }
}
