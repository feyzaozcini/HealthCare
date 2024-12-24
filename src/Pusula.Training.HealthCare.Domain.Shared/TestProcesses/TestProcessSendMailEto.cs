using Volo.Abp.Domain.Entities.Events.Distributed;

namespace Pusula.Training.HealthCare.TestProcesses
{
    public class TestProcessSendMailEto : EtoBase
    {
        public string Email { get; set; }
        public string PatientName { get; set; }
    }
}
