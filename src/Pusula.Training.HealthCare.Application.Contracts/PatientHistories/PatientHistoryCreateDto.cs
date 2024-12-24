using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.PatientHistories
{
    public class PatientHistoryCreateDto
    {
        public Guid PatientId { get; set; }
        public string Habit { get; set; } = null!;

        public string Disease { get; set; } = null!;

        public string Medicine { get; set; } = null!;

        public string Operation { get; set; } = null!;

        public string Vaccination { get; set; } = null!;

        public string Allergy { get; set; } = null!;

        public string SpecialCondition { get; set; } = null!;

        public string Device { get; set; } = null!;

        public string Therapy { get; set; } = null!;

        public string Job { get; set; } = null!;

        public EducationLevel EducationLevel { get; set; }

        public MaritalStatus MaritalStatus { get; set; }
    }
}
