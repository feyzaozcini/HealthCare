using Pusula.Training.HealthCare.TestGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.TestGroupItems;

public class TestGroupItemWithNavigationPropertiesDto
{
    public TestGroupItemDto TestGroupItem { get; set; } = null!;
    public TestGroupDto TestGroup { get; set; } = null!;
}
