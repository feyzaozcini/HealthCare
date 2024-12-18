using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.FollowUpPlans
{
    public class FollowUpPlanConsts
    {
        private const string DefaultSorting = "{0}Note asc";

        public static string GetDefaultSorting(bool withEntityName) => string.Format(DefaultSorting, withEntityName ? "FollowUpPlans." : string.Empty);

        public const int NoteMaxLength = 200;
    }
}
