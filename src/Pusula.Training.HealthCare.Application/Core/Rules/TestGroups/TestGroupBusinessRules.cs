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
    public async Task TestGroupNameDuplicatedAsync(string testGroupName)
    {
        var groupExists = await testGroupRepository.AnyAsync(g => g.Name == testGroupName);
        if (groupExists)
        {
            //Globale çekilecek.
            throw new BusinessException("Bu isimde bir test grubu zaten mevcut.");
        }
    }

    public async Task ValidateTestGroupDeletionAsync(Guid testGroupId)
    {
        var hasRelatedTests = await testGroupItemRepository.AnyAsync(ti => ti.TestGroupId == testGroupId);
        if (hasRelatedTests)
        {
            //Globale çekilecek.
            throw new BusinessException("Bu test grubuna bağlı testler bulunduğundan silinemez.");
        }
    }

    public async Task ValidateTestGroupUpdateAsync(Guid testGroupId)
    {
        var hasActiveTests = await testGroupItemRepository.AnyAsync(ti => ti.TestGroupId == testGroupId);
        if (hasActiveTests)
        {
            //Globale çekilecek.
            throw new BusinessException("Bu test grubuna bağlı testler mevcut. Güncelleme yapılamaz.");
        }
    }
}
