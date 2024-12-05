using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.LabRequests;

public enum RequestStatusEnum
{
    [Display(Name = "İşleme Alındı")]
    InProgress = 1,  
    [Display(Name = "Tamamlandı")]
    Completed = 2,   
    //Cancelled = 3    // İptal Edildi
}
