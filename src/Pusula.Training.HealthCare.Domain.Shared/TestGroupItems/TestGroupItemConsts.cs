using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.TestGroupItems;

public static class TestGroupItemConsts
{
    private const string DefaultSorting = "{0}Name asc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "TestGroupItem." : string.Empty);
    }

    public const int NameMaxLength = 100;
    public const int NameMinLength = 2;

    public const int CodeMaxLength = 20;

    public const int TestTypeMaxLength = 50;

    public const int LabTypeMaxLength = 50;

    public const int DescriptionMaxLength = 500;

    public const int MinTurnaroundTime = 1; 
    public const int MaxTurnaroundTime = 365 * 24; // Maksimum 1 yıl (8760 saat)

    public const string TestGroupItemDeletedMessage = "Test başarıyla silindi.";
    public const string TestSuccessfullyUpdated = "Test başarıyla güncellendi.";
    public const string TestSuccessfullyCreated = "Test başarıyla oluşturuldu.";

}

