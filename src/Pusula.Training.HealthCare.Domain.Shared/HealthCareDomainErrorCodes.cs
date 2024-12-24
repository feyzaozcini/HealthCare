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
    public const string CityNotFound = "Böyle bir şehir adı bulunmamaktadır";
    public const string DistrictNotFound = "Böyle bir ilçe adı bulunmamaktadır";
    public const string VillageNotFound = "Böyle bir köy adı bulunmamaktadır";
  
    //LAB
    public const string TestGroupNameAlreadyExists = "Bu isimde bir test grubu zaten mevcut.";
    public const string TestGroupCannotBeDeleted = "Bu test grubuna bağlı testler bulunduğundan silinemez.";
    public const string TestGroupItemCodeAlreadyExists = "Bu test kodu zaten kullanılıyor.";
    public const string TestGroupItemNameAlreadyExists = "Bu test adı zaten kullanılıyor.";
    public const string TestCannotBeDeletedDueToRequests = "Bu teste ait talepler bulunduğundan silinemez.";
    public const string TestGroupCannotBeChangedDueToActiveRequests = "Bu teste ait aktif talepler bulunduğu için grubu değiştirilemez.";
    public const string TurnaroundTimeCannotBeNegative = "Sonuçlanma süresi sıfırdan küçük olamaz.";
    public const string CannotAddTheSameTestOnTheSameDay = "24 saat içinde aynı test isteminde bulunamazsınız.";


    public const string DiagnosisGroupAlreadyExist = "Böyle bir tanı grubu zaten mevcut";
    public const string DiagnosisAlreadyExist = "Böyle bir tanı zaten mevcut";
    public const string PainTypeAlreadyExist = "Böyle bir Ağrı tipi zaten mevcut";

    //Appointment Modules
    public const string DoctorScheduleConflict = "Bu tarihte doktorun randevusu mevcuttur.";
    public const string DoctorRules = "Doktor için Yaş ve Cinsiyet Kısıtlmasına Uyulmamaktadır.";
    public const string DoctorAlreadyAssignedToAppointmentType = "Doktor zaten bu randevu tipine atanmış.";
    public const string DoctorWorkScheduleConflict = "Doktorun çalışma programı zaten mevcut.";
    public const string PatientWasBlockedByDoctor = "Hasta doktor tarafından engellenmiştir.";
    public const string DublicateBlackListItem = "Hasta zaten doktor tarafından engellenmiştir.";
}
