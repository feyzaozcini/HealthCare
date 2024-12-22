namespace Pusula.Training.HealthCare.Protocols
{
    public static class ProtocolConsts
    {
        private const string DefaultSorting = "{0}StartTime asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Protocol." : string.Empty);
        }

        public const string ProtocolSuccessfullyCreated = "Protocol baþarýyla oluþturuldu.";
        public const string ProtocolSuccessfullyUpdated = "Protocol baþarýyla güncellendi.";
        public const string ProtocolSuccessfullyDeleted = "Protocol baþarýyla silindi.";

    }
}