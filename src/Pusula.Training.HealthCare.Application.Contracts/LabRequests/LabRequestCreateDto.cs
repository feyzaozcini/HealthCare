using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.LabRequests;

public class LabRequestCreateDto
{
    [Required]
    public Guid ProtocolId { get; set; }
    [Required]
    public Guid DoctorId { get; set; }
    [Required]
    public Guid TestGroupItemId { get; set; }
    [Required]
    [StringLength(LabRequestConsts.NameMaxLength)]
    public string Name { get; set; } = null!;
    public DateTime Date { get; set; }
    [Required]
    public RequestStatusEnum Status { get; set; }
}
