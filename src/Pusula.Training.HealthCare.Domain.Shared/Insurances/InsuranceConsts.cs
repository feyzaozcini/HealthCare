using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Insurances
{
    public static class InsuranceConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Insurance." : string.Empty);
        }

        public const int NameMaxLength = 128;

        public const string InsuranceSuccessfullyCreated = "Sigorta başarıyla oluşturuldu.";
        public const string InsuranceSuccessfullyUpdated = "Sigorta başarıyla güncellendi.";
        public const string InsuranceSuccessfullyDeleted = "Sigorta başarıyla silindi.";
    }
}
