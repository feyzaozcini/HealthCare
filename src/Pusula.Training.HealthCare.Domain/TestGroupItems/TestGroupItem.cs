using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Pusula.Training.HealthCare.TestGroupItems;

public class TestGroupItem : AuditedEntity<Guid>
{
    public Guid TestGroupId { get; private set; } // Set yalnızca sınıf içinde erişilebilir.

    [NotNull]
    public string Name { get; private set; } = null!;
    [NotNull]

    public string Code { get; private set; } = null!;

    [NotNull]
    public string TestType { get; private set; } = null!; // Test tipi, arayüzde kullanılacak. Senaryosu gerekirse tasarlanacak.
    public string? Description { get; private set; } 
    public int? TurnaroundTime { get; set; } // Tetkikin tahmini tamamlanma süresi. Saat cinsinden tanımlanacak. Arayüzde gün ve saat dönüşümü yapılacak.

    protected TestGroupItem()
    {
    }

    public TestGroupItem(
        Guid id,
        Guid testGroupId,
        string name,
        string code,
        string testType,
        string? description,
        int? turnaroundTime)
    {
        Id = id;
        SetTestGroupId(testGroupId);
        SetName(name);
        SetCode(code);
        SetTestType(testType);
        SetDescription(description);
        SetTurnaroundTime(turnaroundTime);
    }

    public void SetTestGroupId(Guid testGroupId)
    {
        TestGroupId = Check.NotNull(testGroupId, nameof(testGroupId));
    }

    public void SetName(string name)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.Length(name, nameof(name), TestGroupItemConsts.NameMaxLength, TestGroupItemConsts.NameMinLength);
        Name = name;
    }

    public void SetCode(string code)
    {
        Check.Length(code, nameof(code), TestGroupItemConsts.CodeMaxLength);
        Code = code;
    }

    public void SetTestType(string testType)
    {
        Check.Length(testType, nameof(testType), TestGroupItemConsts.TestTypeMaxLength);
        TestType = testType;
    }

    public void SetDescription(string? description)
    {
        Check.Length(description, nameof(description), TestGroupItemConsts.DescriptionMaxLength);
        Description = description;
    }
    public void SetTurnaroundTime(int? turnaroundTime)
    {
        TurnaroundTime = turnaroundTime;
    }
}

