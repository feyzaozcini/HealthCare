namespace Pusula.Training.HealthCare.Countries;

public static class CountryConsts
{
    private const string DefaultSorting = "{0}Name asc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "Country." : string.Empty);
    }

    public const int NameMaxLength = 60;
    public const int NameMinLength = 3;

    public const int CodeMinLength = 2;
    public const int CodeMaxLength = 4;

}
