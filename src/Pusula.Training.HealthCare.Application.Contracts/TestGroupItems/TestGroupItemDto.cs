using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.TestGroupItems;

public class TestGroupItemDto : AuditedEntityDto<Guid>
{
    [Required]
    public Guid TestGroupId { get; set; }

    public string TestGroupName { get; set; } = null!;
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Code { get; set; } = null!;

    [Required]
    public string TestType { get; set; } = null!; 
    public string? Description { get; set; }
    [Required]
    public int TurnaroundTime { get; set; }
}
