using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Pusula.Training.HealthCare.Services;

public interface IMernisValidationService : ITransientDependency
{
    Task<bool> ValidateIdentityAsync(string nationalId, string firstName, string lastName, int birthYear);

}
