using JetBrains.Annotations;
using Pusula.Training.HealthCare.TestGroupItems;
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.TestGroups;

public class TestGroup : AuditedEntity<Guid>
{
    [NotNull]
    public string Name { get; private set; } = null!; // Set yalnızca sınıf içinde erişilebilir.

    protected TestGroup()
    {
    }

    public TestGroup(Guid id, string name)
    {
        Id = id;
        SetName(name);
    }

    public void SetName(string name)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.Length(name, nameof(name), TestGroupConsts.NameMaxLength, TestGroupConsts.NameMinLength);
        Name = name;
    }
}
