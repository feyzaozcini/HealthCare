using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.TestProcesses;

public class TestProcessesCreateDto
{
    [Required]
    public Guid LabRequestId { get; set; }
    [Required]
    public Guid TestGroupItemId { get; set; }
    [Required]
    public TestProcessStates Status { get; set; }
    public decimal? Result { get; set; }
    public DateTime? ResultDate { get; set; }
}
