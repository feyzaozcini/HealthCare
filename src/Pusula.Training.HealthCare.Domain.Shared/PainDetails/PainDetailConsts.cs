using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.PainDetails
{
    public class PainDetailConsts
    {
        private const string DefaultSorting = "{0}Score asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "PainDetail." : string.Empty);
        }

        public const int DescriptionMaxLength = 50;
        public const int OtherPainMaxLength = 30;
        public const int AreaMaxLength = 25;
        public const int ScoreMax = 10;



    }
}
