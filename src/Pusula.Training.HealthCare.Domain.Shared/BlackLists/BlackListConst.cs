using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.BlackLists
{
    public class BlackListConst
    {
        private const string DefaultSorting = "{0}Note asc";

        public static string GetDefaultSorting(bool withEntityName) => string.Format(DefaultSorting, withEntityName ? "BlackList." : string.Empty);

    }
}
