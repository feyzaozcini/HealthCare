using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Pusula.Training.HealthCare.TestGroups;

public class TestGroupManager(ITestGroupRepository testGroupRepository) : DomainService
{
    public virtual async Task<TestGroup> CreateAsync(
        string name
        )
    {
        Check.NotNull(name, nameof(name));
        Check.Length(name, nameof(name), TestGroupConsts.NameMaxLength, TestGroupConsts.NameMinLength);

        var testGroup = new TestGroup(
         GuidGenerator.Create(),
         name
        );

        return await testGroupRepository.InsertAsync(testGroup);
    }

    public virtual async Task<TestGroup> UpdateAsync(
        Guid id,
        string name
        )
    {
        var testGroup = await testGroupRepository.GetAsync(id);
        testGroup.SetName(name);
        return await testGroupRepository.UpdateAsync(testGroup);
    }
}
