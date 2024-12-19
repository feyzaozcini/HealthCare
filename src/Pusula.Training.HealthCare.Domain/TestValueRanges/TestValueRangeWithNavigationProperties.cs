using Pusula.Training.HealthCare.TestGroupItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.TestValueRanges;

public class TestValueRangeWithNavigationProperties
{
    public TestValueRange? TestValueRange { get; set; }
    public TestGroupItem? TestGroupItem { get; set; }
}
