using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Cities
{
    public static class CityConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "City." : string.Empty);
        }

        public const int NameMaxLength = 60;
    }
}
