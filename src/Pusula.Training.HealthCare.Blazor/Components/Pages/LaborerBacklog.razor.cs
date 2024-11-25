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
        new { PatientName = "Ömer Çelik", Info = "A1ACT Ag Test", DoctorName = "Ali Mutlu", Date = DateTime.Now, Status = "Hazýrlanýyor", Result = 11.2m },
        new { PatientName = "Ayþe Çelik", Info = "Beta-2-Microglobulin Ag Test", DoctorName = "Hakan Mutlu", Date = DateTime.Now, Status = "Tamamlandý", Result = 9.2m },
        new { PatientName = "Filiz Çelik", Info = "Carcinoembryonic Ag Test", DoctorName = "Þükrü Mutlu", Date = DateTime.Now, Status = "Bekleniyor", Result = 80.5m }
    };

    }
}