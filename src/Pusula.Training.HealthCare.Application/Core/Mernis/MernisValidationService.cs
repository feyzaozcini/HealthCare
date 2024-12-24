using ServiceReference;
using System;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Services;

public class MernisValidationService : IMernisValidationService
{
    public async Task<bool> ValidateIdentityAsync(string nationalId, string firstName, string lastName, int birthYear)
    {
        try
        {
            // Mernis SOAP istemcisini oluştur
            using var client = new KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);

            // Doğrulama isteği
            var result = await client.TCKimlikNoDogrulaAsync(new TCKimlikNoDogrulaRequest
            {
                Body = new TCKimlikNoDogrulaRequestBody
                {
                    TCKimlikNo = long.Parse(nationalId),
                    Ad = firstName.ToUpper(),
                    Soyad = lastName.ToUpper(),
                    DogumYili = birthYear
                }
            });

            return result.Body.TCKimlikNoDogrulaResult;
        }
        catch (Exception ex)
        {
            // Hata yönetimi
            Console.WriteLine($"Mernis doğrulama sırasında hata oluştu: {ex.Message}");
            return false;
        }
    }
}
