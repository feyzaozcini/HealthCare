using System.ComponentModel;

namespace Pusula.Training.HealthCare.TestProcesses;

public enum TestProcessStates
{
    [Description("Talep Edildi")]
    Requested = 1,           // Test talebi oluşturuldu

    [Description("Tamamlandı")]
    Approved = 2,           // Test tamamlandı

    [Description("İptal Edildi")]
    Completed = 3,           // Test iptal edildi
}
