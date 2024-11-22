using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.PshychologicalStates
{
    public class PshychologicalStateConsts
    {
        private const string DefaultSorting = "{0}ProtocolId asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "PhychologicalState." : string.Empty);
        }

        public const int DescriptionMaxLength = 128;
      
    }
}
