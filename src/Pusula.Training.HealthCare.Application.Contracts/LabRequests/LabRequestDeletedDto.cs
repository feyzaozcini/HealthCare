using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.LabRequests;

public class LabRequestDeletedDto
{
    public Guid Id { get; set; }
    public string? Message { get; set; }
}
