using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.ExaminationDiagnoses
{
    public class ExaminationDiagnosisConsts
    {
        private const string DefaultSorting = "{0}InitialDate desc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ExaminationDiagnosis." : string.Empty);
        }

        public const int NoteMaxLength = 75;
    }
}
