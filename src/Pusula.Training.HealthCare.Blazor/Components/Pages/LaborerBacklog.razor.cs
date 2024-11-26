using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Blazor.Components.Pages
{
    public partial class LaborerBacklog
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        private List<object> GridData = new List<object>
    {
        new { PatientName = "Mehmet KAYA", Info = "Glucose Test", DoctorName = "Ahmet YILMAZ", Date = DateTime.Now, Status = "Tamamlandi", Result = 95.4m },
new { PatientName = "Selim CAN", Info = "Cholesterol Test", DoctorName = "Huseyin AKSU", Date = DateTime.Now, Status = "Hazirlaniyor", Result = 203.1m },
new { PatientName = "Fatma YILMAZ", Info = "Vitamin D Test", DoctorName = "Zehra ERDEM", Date = DateTime.Now, Status = "Tamamlandi", Result = 34.2m },
new { PatientName = "Hasan CELIK", Info = "Hemoglobin A1C Test", DoctorName = "Kemal ASLAN", Date = DateTime.Now, Status = "Bekleniyor", Result = 6.8m },
new { PatientName = "Aylin DURMAZ", Info = "Thyroid Panel Test", DoctorName = "Leyla CIFT", Date = DateTime.Now, Status = "Tamamlandi", Result = 1.7m },
new { PatientName = "Umut SEN", Info = "Iron Level Test", DoctorName = "Omer YAVUZ", Date = DateTime.Now, Status = "Hazirlaniyor", Result = 110.3m },
new { PatientName = "Cem TUNC", Info = "Liver Function Test", DoctorName = "Duygu YAZAR", Date = DateTime.Now, Status = "Bekleniyor", Result = 32.9m },
new { PatientName = "Ece OZDEMIR", Info = "Kidney Function Test", DoctorName = "Murat DEMIR", Date = DateTime.Now, Status = "Tamamlandi", Result = 78.6m },
new { PatientName = "Eren ALTIN", Info = "Electrolyte Panel Test", DoctorName = "Caner BULUT", Date = DateTime.Now, Status = "Hazirlaniyor", Result = 142.5m },
new { PatientName = "Sevgi ILHAN", Info = "Blood Count Test", DoctorName = "Elif AKIN", Date = DateTime.Now, Status = "Tamamlandi", Result = 13.8m }

    };

    }
}