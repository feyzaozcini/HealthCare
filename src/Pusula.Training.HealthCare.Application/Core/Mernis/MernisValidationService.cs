using Pusula.Training.HealthCare.Exceptions;
using ServiceReference;
using System;
using System.Threading.Tasks;
using Volo.Abp;

namespace Pusula.Training.HealthCare.Services;

public class MernisValidationService : IMernisValidationService
{
    public async Task<bool> ValidateIdentityAsync(IdentityValidationDto dto)
    {
        try
        {
            using var client = new KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);

            var result = await client.TCKimlikNoDogrulaAsync(new TCKimlikNoDogrulaRequest
            {
                Body = new TCKimlikNoDogrulaRequestBody
                {
                    TCKimlikNo = long.Parse(dto.NationalId),
                    Ad = dto.FirstName.ToUpper(),
                    Soyad = dto.LastName.ToUpper(),
                    DogumYili = dto.BirthYear
                }
            });

            return result.Body.TCKimlikNoDogrulaResult;
        }
        catch (UserFriendlyException ex)
        {
            HealthCareException.ThrowIf(
                HealthCareDomainErrorCodes.MernisVerificationError
            );
            return false;
        }
    }
}
