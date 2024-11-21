using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.PhysicalExaminations
{
    public class PhysicalExaminationUpdateDto
    {
        public Guid Id { get; set; } = default!;
        public decimal? Weight { get; set; }
        public decimal? Height { get; set; }
        public decimal? BMI { get; set; }
        public decimal? VYA { get; set; }
        public decimal? Temperature { get; set; }
        public int? Pulse { get; set; }
        public int? SystolicBP { get; set; }
        public int? DiastolicBP { get; set; }
        public int? SPO2 { get; set; }
        public string Note { get; set; } = null!;

        public Guid ProtocolId { get; set; }
    }
}
