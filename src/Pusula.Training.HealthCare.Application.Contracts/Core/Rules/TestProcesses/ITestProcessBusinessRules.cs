using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Core.Rules.TestProcesses;

public interface ITestProcessBusinessRules : IBusinessRules
{
    Task ValidateRecentTestsAsync(Guid labRequestId, Guid testGroupItemId);
}
