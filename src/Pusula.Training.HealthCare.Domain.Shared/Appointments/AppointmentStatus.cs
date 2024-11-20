using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Appointments
{
    public enum AppointmentStatus
    {
        Scheduled = 1,      // Planlanmış
        Confirmed = 2,      // Onaylı
        Completed = 3,      // Tamamlanmış
        Cancelled = 4,      // İptal Edilmiş
        Rescheduled = 5,    // Yeniden Planlanmış
        NoShow = 6,         // Gelmedi
        Pending = 7,        // Beklemede
        InProgress = 8,     // Devam Ediyor
        Postponed = 9,      // Ertelenmiş
        Failed = 10         // Başarısız
    }
}
