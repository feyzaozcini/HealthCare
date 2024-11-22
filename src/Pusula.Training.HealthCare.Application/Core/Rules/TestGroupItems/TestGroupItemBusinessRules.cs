using Pusula.Training.HealthCare.Exceptions;
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
    //Aynı test kodu eklenemez.
    public async Task TestGroupItemCodeDuplicatedAsync(string code)
    {
        HealthCareException.ThrowIf(
            HealthCareDomainErrorCodes.TestGroupItemCodeAlreadyExists,
            await testGroupItemRepository.AnyAsync(ti => ti.Code == code)
        );
    }

    //Teste ait talepler varsa test silinemez.
    public async Task ValidateTestDeletableAsync(Guid testItemId)
    {
        HealthCareException.ThrowIf(
            HealthCareDomainErrorCodes.TestCannotBeDeletedDueToRequests,
            await labRequestRepository.AnyAsync(lr => lr.TestGroupItemId == testItemId)
        );
    }

    //Teste ait talepler varsa grubu güncellenemez.
    public async Task ValidateTestGroupChangeAllowedAsync(Guid testItemId, Guid newGroupId)
    {
        HealthCareException.ThrowIf(
            HealthCareDomainErrorCodes.TestGroupCannotBeChangedDueToActiveRequests,
            await labRequestRepository.AnyAsync(lr => lr.TestGroupItemId == testItemId)
        );
    }

    //Testin sonuçlanma süresi 0 saatten az olamaz.
    public Task ValidateMinimumTurnaroundTimeAsync(decimal turnaroundTime)
    {
        HealthCareException.ThrowIf(
            HealthCareDomainErrorCodes.TurnaroundTimeCannotBeNegative,
            turnaroundTime < 0
        );

        return Task.CompletedTask; // Synchronous operation returned as Task
    }

    public async Task TestGroupItemNameDuplicatedAsync(string name)
    {
        HealthCareException.ThrowIf(
           HealthCareDomainErrorCodes.TestGroupItemNameAlreadyExists,
           await testGroupItemRepository.AnyAsync(ti => ti.Name == name)
       );
    }
}
