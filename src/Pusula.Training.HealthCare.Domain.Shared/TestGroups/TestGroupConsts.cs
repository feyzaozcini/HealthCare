using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.TestGroups;

public class TestGroupConsts
{
    private const string DefaultSorting = "{0}Name asc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "TestGroup." : string.Empty);
    }

    public const int NameMaxLength = 50;
    public const int NameMinLength = 2;

    public const string TestGroupDeletedMessage = "Test grubu silindi.";
}
