using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Services;
using Volo.Abp.Identity;

namespace Pusula.Training.HealthCare.UserProfiles;

public class UserProfileManager(UserManager<IdentityUser> userManager, IIdentityRoleRepository identityRoleRepository) : DomainService, IUserProfileManager
{
    public async Task<IdentityUser> CreateUserWithPropertiesAsync(
    string userName,
    string name,
    string surname,
    string email,
    string password,
    string role = null!,
    string phoneNumber = null!,
    bool isEmailConfirmed = false
)
    {
        // Burada yeni bir user nesnesi oluşturuyoruz.
        var user = new IdentityUser(GuidGenerator.Create(), userName, email, CurrentTenant.Id)
        {
            Name = name,
            Surname = surname
        };

        // Oluşturduğumuz user nesnesi ile gerçek bir user oluşturma işlemi yapıyoruz.
        var createUserResult = await userManager.CreateAsync(user, password);

        // Burada telefon numarasını ayarlıyoruz.
        if (!string.IsNullOrEmpty(phoneNumber))
        {
            var setPhoneResult = await userManager.SetPhoneNumberAsync(user, phoneNumber);
            if (!setPhoneResult.Succeeded)
            {
                throw new BusinessException("SetPhoneNumberFailed")
                    .WithData("Errors", string.Join(", ", setPhoneResult.Errors.Select(e => e.Description)));
            }
        }


        // Burada oluşturulan kullanıcıya bir rol ekliyoruz.
        var addToRoleResult = await userManager.AddToRoleAsync(user, role);
        if (!addToRoleResult.Succeeded)
        {
            throw new BusinessException("AddToRoleFailed")
                .WithData("Errors", string.Join(", ", addToRoleResult.Errors.Select(e => e.Description)));
        }

        return user;
    }
}
