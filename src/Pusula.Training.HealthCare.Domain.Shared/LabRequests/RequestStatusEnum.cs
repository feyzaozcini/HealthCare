using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.LabRequests;

public enum RequestStatusEnum
{
    InProgress = 1,  // İşlemde
    Completed = 2,   // Tamamlandı
    Cancelled = 3    // İptal Edildi
}
