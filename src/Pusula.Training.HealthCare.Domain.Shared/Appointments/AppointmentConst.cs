using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Appointments
{
    public class AppointmentConst
    {
        private const string DefaultSorting = "{0}AppointmentDate asc";

        public static string GetDefaultSorting(bool withEntityName) => string.Format(DefaultSorting, withEntityName ? "Appointment." : string.Empty);

        public const int NoteMaxLength = 500;
    }
}
