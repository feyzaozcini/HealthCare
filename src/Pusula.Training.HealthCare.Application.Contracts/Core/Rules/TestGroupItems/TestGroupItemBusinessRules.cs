using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Core.Rules.TestGroupItems;

public interface ITestGroupItemBusinessRules : IBusinessRules
{
    Task TestGroupItemCodeDuplicatedAsync(string code);
    Task TestGroupItemNameDuplicatedAsync(string name);
    Task ValidateMinimumTurnaroundTimeAsync(decimal turnaroundTime);
}
