using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.TestProcesses;

public enum TestProcessStates
{
    [Display(Name = "Talep Edildi")]
    Requested = 1,

    [Display(Name = "Tamamlandı")]
    Approved = 2,        

    //[Description("İptal Edildi")]
    //Completed = 3,           // Test iptal edildi
}
