namespace Pusula.Training.HealthCare.Protocols
{
    public static class ProtocolConsts
    {
        private const string DefaultSorting = "{0}StartTime asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Protocol." : string.Empty);
        }

        public const string ProtocolSuccessfullyCreated = "Protocol ba�ar�yla olu�turuldu.";
        public const string ProtocolSuccessfullyUpdated = "Protocol ba�ar�yla g�ncellendi.";
        public const string ProtocolSuccessfullyDeleted = "Protocol ba�ar�yla silindi.";

    }
}