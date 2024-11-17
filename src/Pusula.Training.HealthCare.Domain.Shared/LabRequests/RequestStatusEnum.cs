using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.LabRequests;

public enum RequestStatusEnum
{
    Pending = 1,     // Bekliyor
    InProgress = 2,  // İşlemde
    Completed = 3,   // Tamamlandı
    Cancelled = 4    // İptal Edildi
}
