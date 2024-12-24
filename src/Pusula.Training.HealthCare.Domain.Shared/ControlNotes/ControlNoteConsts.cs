using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.ControlNotes
{
    public class ControlNoteConsts
    {
        private const string DefaultSorting = "{0}Note asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ControlNote." : string.Empty);
        }

        public const int NoteMaxLength = 250;
    }
}
