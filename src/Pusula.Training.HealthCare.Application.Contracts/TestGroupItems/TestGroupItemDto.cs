using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.TestGroupItems;

public class TestGroupItemDto : AuditedEntityDto<Guid>
{
    public Guid TestGroupId { get; set; } 
    public string Name { get; private set; } = null!;
    public string Code { get; private set; } = null!;
    public string TestType { get; private set; } = null!; 
    public string? Description { get; private set; }
    public int? TurnaroundTime { get; set; }
}
