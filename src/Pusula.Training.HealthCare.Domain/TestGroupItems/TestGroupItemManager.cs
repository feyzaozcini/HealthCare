using Pusula.Training.HealthCare.TestGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.TestGroupItems;

public class TestGroupItemManager(ITestGroupItemRepository testGroupItemRepository) : DomainService
{
    public virtual async Task<TestGroupItem> CreateAsync(
        Guid testGroupId,
        string name,
        string code,
        string testType,
        string? description,
        int? turnaroundTime
        )
    {
        
        var testGroupItem = new TestGroupItem(
        GuidGenerator.Create(),
        testGroupId,
        name,
        code,
        testType,
        description,
        turnaroundTime
        );

        return await testGroupItemRepository.InsertAsync(testGroupItem);
    }

    public virtual async Task<TestGroupItem> UpdateAsync(
        Guid id,
        Guid testGroupId,
        string name,
        string code,
        string testType,
        string? description,
        int? turnaroundTime
        )
    {
        var testGroupItem = await testGroupItemRepository.GetAsync(id);
        testGroupItem.SetTestGroupId(testGroupId);
        testGroupItem.SetName(name);
        testGroupItem.SetCode(code);
        testGroupItem.SetTestType(testType);
        testGroupItem.SetDescription(description);
        testGroupItem.SetTurnaroundTime(turnaroundTime);
        return await testGroupItemRepository.UpdateAsync(testGroupItem);
    }
}
