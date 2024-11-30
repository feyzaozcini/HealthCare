using Pusula.Training.HealthCare.LabRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.DiagnosisGroups
{
    public class DiagnosisGroupConsts
    {
        private const string DefaultSorting = "{0}Code asc";
        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "DiagnosisGroup." : string.Empty);
        }

        public const int NameMaxLength = 100;
        public const int NameMinLength = 2;
        public const int CodeMaxLength = 5;
        public const int CodeMinLength = 2;
       

        
    }
}
