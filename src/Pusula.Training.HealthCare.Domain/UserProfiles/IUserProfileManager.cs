using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace Pusula.Training.HealthCare.UserProfiles;

public interface IUserProfileManager : ITransientDependency
{
    Task<IdentityUser> CreateUserWithPropertiesAsync(
        string userName,
        string name,
        string surname,
        string email,
        string password,
        string role = null!,
        string phoneNumber = null!,
        bool isEmailConfirmed = false
        );
}
