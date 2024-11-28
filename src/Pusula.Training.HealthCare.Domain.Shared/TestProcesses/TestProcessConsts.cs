using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.TestProcesses;

public class TestProcessConsts
{
    private const string DefaultSorting = "{0}ResultDate asc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "TestProcess." : string.Empty);
    }

    public const string TestProcessDeletedMessage = "Silme işlemi başarılı";

}
