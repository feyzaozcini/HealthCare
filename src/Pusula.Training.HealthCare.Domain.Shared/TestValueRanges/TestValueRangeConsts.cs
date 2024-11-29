using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.TestValueRanges;

public class TestValueRangeConsts
{
    private const string DefaultSorting = "{0}MinValue asc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "TestValueRange." : string.Empty);
    }

    public const string TestValueRangeDeletedMessage = "Silme işlemi başarılı";

}


