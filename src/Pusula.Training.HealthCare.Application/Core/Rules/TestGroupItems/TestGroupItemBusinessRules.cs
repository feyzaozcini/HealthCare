using Pusula.Training.HealthCare.LabRequests;
using Pusula.Training.HealthCare.TestGroupItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Core.Rules.TestGroupItems;

public class TestGroupItemBusinessRules(ITestGroupItemRepository testGroupItemRepository, ILabRequestRepository labRequestRepository) : ITestGroupItemBusinessRules
{
    public async Task ValidateTestItemCreationAsync(string code)
    {
        var codeExists = await testGroupItemRepository.AnyAsync(ti => ti.Code == code);
        if (codeExists)
        {
            //Globale çekilecek.
            throw new BusinessException("Bu test kodu zaten kullanılıyor.");
        }
    }

    public async Task ValidateTestItemDeletionAsync(Guid testItemId)
    {
        var hasActiveRequests = await labRequestRepository.AnyAsync(lr => lr.Id == testItemId);
        if (hasActiveRequests)
        {
            //Globale çekilecek.
            throw new BusinessException("Bu teste ait talepler bulunduğundan silinemez.");
        }
    }

    public async Task ValidateTestItemUpdateAsync(Guid testItemId, Guid newGroupId)
    {
        var hasActiveRequests = await labRequestRepository.AnyAsync(lr => lr.Id == testItemId);
        if (hasActiveRequests)
        {
            //Globale çekilecek.
            throw new BusinessException("Bu teste ait aktif talepler bulunduğu için grubu değiştirilemez.");
        }
    }

    public void ValidateTurnaroundTime(decimal turnaroundTime)
    {
        if (turnaroundTime < 0)
        {
            //Globale çekilecek.
            throw new BusinessException("Sonuçlanma süresi sıfırdan küçük olamaz.");
        }
    }
}
