using Pusula.Training.HealthCare.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.PhysicalExaminations
{
    public class PhysicalExaminationWithNavigationPropertiesDto
    {
        public PhysicalExaminationDto PhysicalExamiantion { get; set; } = null!;

        public ProtocolDto Protocol { get; set; } = null!;


    }
}
