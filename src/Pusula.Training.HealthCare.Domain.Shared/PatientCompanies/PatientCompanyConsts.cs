using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.PatientCompanies
{
    public static class PatientCompanyConsts
    {
        private const string  DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName) => string.Format(DefaultSorting, withEntityName ? "PatientCompany" : string.Empty);


        public const int NameMinLength = 3;
        public const int NameMaxLength = 60;
    }
}
