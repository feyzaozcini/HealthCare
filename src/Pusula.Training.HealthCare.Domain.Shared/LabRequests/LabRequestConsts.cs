using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.LabRequests;

public static class LabRequestConsts
{
    private const string DefaultSorting = "{0}Date desc";
    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "LabRequest." : string.Empty);
    }

    public const int NameMaxLength = 100;
    public const int NameMinLength = 2;
    public const RequestStatusEnum DefaultStatus = RequestStatusEnum.Pending; // Varsayılan durum

    //public const int MaxRequestsPerDay = 100; // Örneğin, bir gün için maksimum talep sayısı

    public const string LabRequestDeletedMessage = "Laboratuvar isteği silindi.";

}

