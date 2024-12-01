using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Notes
{
    public static class NoteConsts
    {
        private const string DefaultSorting = "{0}Text asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Note." : string.Empty);
        }

        public const int TextMaxLength = 256;
    }
}
