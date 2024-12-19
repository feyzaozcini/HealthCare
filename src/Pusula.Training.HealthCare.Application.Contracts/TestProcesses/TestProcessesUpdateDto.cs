using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.TestProcesses;

public class TestProcessesUpdateDto : TestProcessesCreateDto
{
    public Guid Id { get; set; }
}
