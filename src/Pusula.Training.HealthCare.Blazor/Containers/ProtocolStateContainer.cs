using System;

namespace Pusula.Training.HealthCare.Blazor.Containers
{
    public class ProtocolStateContainer
    {
        public Guid ProtocolId { get; private set; }
        public Guid PatientId { get; private set; }

        public event Action OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();

        public void SetProtocol(Guid protocolId, Guid patientId)
        {
            ProtocolId = protocolId;
            PatientId = patientId;
            NotifyStateChanged();
        }

        public void ClearProtocol()
        {
            ProtocolId = Guid.Empty;
            PatientId = Guid.Empty;
        }
    }
}
