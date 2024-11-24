using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Doctors
{
    public class DoctorConsts
    {

        private const string DefaultSorting = "{0}identityNumber asc";

        public static string GetDefaultSorting(bool withEntityName) => string.Format(DefaultSorting, withEntityName ? "Doctor." : string.Empty);

        public const int IdentityNumberMaxLength = 11;
    }
}
