using Microsoft.Extensions.Localization;
using System;

namespace Pusula.Training.HealthCare.Extensions;

public static class EnumExtensions
{
    public static string GetLocalizedDisplayName<TEnum>(this TEnum enumValue, IStringLocalizer localizer) where TEnum : Enum
    {
        var enumType = typeof(TEnum).Name;
        var enumKey = $"Enums.{enumType}.{enumValue}";

        return localizer[enumKey];
    }
}
