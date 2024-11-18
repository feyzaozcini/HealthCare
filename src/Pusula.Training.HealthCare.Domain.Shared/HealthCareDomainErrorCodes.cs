namespace Pusula.Training.HealthCare;

public static class HealthCareDomainErrorCodes
{
    public const string IdentityNumberAlreadyExists = "Bu TC kimlik numarasına sahip bir hasta zaten mevcut.";

    public const string DeleteMessage = "Silme İşlemi Başarılı";

    public const string PatientNotFound = "Hasta bulunamadı.";

    public const string PatientCompanyNameExist = "Böyle bir kurum zaten mevcut. Lütfen varolan kurumlardan seçiniz.";

    public const string PatientCompanyNameNotFound = "Böyle bir kurum zaten mevcut. Lütfen varolan kurumlardan seçiniz.";

    public const string CountryNameExists = "Böyle bir ülke adı bulunmaktadır.";

    public const string CountryNotFound = "Böyle bir ülke adı bulunmamaktadır";
}
