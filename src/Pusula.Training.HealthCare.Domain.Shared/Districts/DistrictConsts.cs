using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Districts
{
    public static class DistrictConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "District." : string.Empty);
        }

        public const int NameMaxLength = 60;
    }
}
