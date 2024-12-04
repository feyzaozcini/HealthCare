using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Core.Rules.Diagnoses
{
    public interface IDiagnosisBusinessRules : IBusinessRules
    {
        Task DiagnosisCodeDuplicatedAsync(string diagnosisCode);

    }
}
