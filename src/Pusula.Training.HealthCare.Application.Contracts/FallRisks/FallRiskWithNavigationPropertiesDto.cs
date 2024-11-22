using Pusula.Training.HealthCare.Protocols;

namespace Pusula.Training.HealthCare.FallRisks
{
    public class FallRiskWithNavigationPropertiesDto
    {
        public FallRiskDto FallRisk { get; set; } = null!;

        public ProtocolDto Protocol { get; set; } = null!;
    }
}
