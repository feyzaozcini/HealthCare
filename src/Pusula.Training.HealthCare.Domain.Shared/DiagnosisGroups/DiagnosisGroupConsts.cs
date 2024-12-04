namespace Pusula.Training.HealthCare.DiagnosisGroups
{
    public class DiagnosisGroupConsts
    {
        private const string DefaultSorting = "{0}Code asc";
        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "DiagnosisGroup." : string.Empty);
        }

        public const int NameMaxLength = 100;
        public const int NameMinLength = 2;
        public const int CodeMaxLength = 5;
        public const int CodeMinLength = 2;

        public const string CodeRequired = "Tanı grubu kodu doldurulması zorunludur";
        public const string CodeLength = "Tanı grubu kodu en az 2 en fazla 5 karakter icermelidir";

        public const string NameRequired = "Tanı grubu adı doldurulması zorunludur";
        public const string NameLength = "Tanı grubu adı en az 2 en fazla 100 karakter icermelidir";



    }
}
