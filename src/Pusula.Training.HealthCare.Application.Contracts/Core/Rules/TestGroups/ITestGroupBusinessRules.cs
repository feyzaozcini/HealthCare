using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Core.Rules.TestGroups;

public interface ITestGroupBusinessRules : IBusinessRules
{
    Task TestGroupNameDuplicatedAsync(string testGroupName);
    Task ValidateTestGroupDeletableAsync(Guid testGroupId);
}
