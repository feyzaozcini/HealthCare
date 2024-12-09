using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Core.DTOs;

public class CounterDto
{
    public string TestName { get; set; } = null!;
    public int Count { get; set; }
}
