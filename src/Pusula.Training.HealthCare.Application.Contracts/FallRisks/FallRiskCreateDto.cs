using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.FallRisks
{
    public class FallRiskCreateDto
    {
        public Guid ProtocolId { get; set; }

        public int Score { get; set; }
        public string Description { get; set; } = null!;
        public bool HasFallHistory { get; set; }
        public bool UsesMedications { get; set; }
        public bool HasAddiction { get; set; }
        public bool HasBalanceDisorder { get; set; }
        public bool HasVisionImpairment { get; set; }
        public bool MentalState { get; set; }
        public bool GeneralHealthState { get; set; }
    }
}
