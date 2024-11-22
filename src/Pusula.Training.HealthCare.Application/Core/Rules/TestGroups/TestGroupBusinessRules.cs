using Pusula.Training.HealthCare.Exceptions;
using Pusula.Training.HealthCare.TestGroupItems;
using Pusula.Training.HealthCare.TestGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Core.Rules.TestGroups;

public class TestGroupBusinessRules(ITestGroupRepository testGroupRepository, ITestGroupItemRepository testGroupItemRepository) : ITestGroupBusinessRules
{
    //Aynı isimde yeni bir test grubu eklenemez.
    public async Task TestGroupNameDuplicatedAsync(string testGroupName)
    {
        HealthCareException.ThrowIf(
            HealthCareDomainErrorCodes.TestGroupNameAlreadyExists,
            await testGroupRepository.AnyAsync(g => g.Name == testGroupName)
        );
    }

    //Test grubuna bağlı testler varsa, silme işlemi geçerli olmaz.
    public async Task ValidateTestGroupDeletableAsync(Guid testGroupId)
    {
        HealthCareException.ThrowIf(
            HealthCareDomainErrorCodes.TestGroupCannotBeDeleted,
            await testGroupItemRepository.AnyAsync(ti => ti.TestGroupId == testGroupId)
        );
    }
}
