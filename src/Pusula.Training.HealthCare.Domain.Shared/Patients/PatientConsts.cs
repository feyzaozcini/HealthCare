namespace Pusula.Training.HealthCare.Patients
{
    public static class PatientConsts
    {
        private const string DefaultSorting = "{0}FirstName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Patient." : string.Empty);
        }

        public const int FirstNameMaxLength = 128;
        public const int LastNameMaxLength = 128;
        public const int IdentityNumberMaxLength = 11;
        public const int PassportNumberMinLength = 6;
        public const int PassportNumberMaxLength = 12;
        public const int EmailAddressMaxLength = 128;
        public const int PhoneNumberMinLength = 10;  
        public const int PhoneNumberMaxLength = 15;
        public const int PatientCompanyMaxLength = 60;

        public const string PatientSuccessfullyCreated = "Hasta ba�ar�yla olu�turuldu.";
        public const string PatientSuccessfullyDeleted = "Hasta ba�ar�yla silindi.";
        public const string PatientSuccessfullyUpdated = "Hasta ba�ar�yla g�ncellendi.";

    }
}