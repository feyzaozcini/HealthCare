using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Core.Rules.TestGroupItems;

public interface ITestGroupItemBusinessRules : IBusinessRules
{
    Task ValidateTestItemCreationAsync(string code);
    void ValidateTurnaroundTime(decimal turnaroundTime);
    Task ValidateTestItemUpdateAsync(Guid testItemId, Guid newGroupId);
    Task ValidateTestItemDeletionAsync(Guid testItemId);
}
