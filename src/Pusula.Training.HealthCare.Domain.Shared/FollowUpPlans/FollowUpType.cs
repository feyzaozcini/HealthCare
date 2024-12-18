using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.FollowUpPlans
{
    public enum FollowUpType
    {
        [Display(Name = "Takip onerilmedi")]
        NoFollowUp = 1, // Belirtilmemiş

        [Display(Name = "Ayaktan Takip")]
        AmbulatoryTreatment = 2,// ayaktan takip ve tedavi   

        [Display(Name = "Yatarak takip ev tedavi")]
        InpatientTreatment = 3,// yatarak takip ve tedavi

        [Display(Name = "ayaktan cerrahi gírisim")]
        AmbulatorySurgery = 4, // ayaktan cerrahi gírisim

        [Display(Name = "yatarak cerrahi gírisim")]
        InpatientSurgery = 5,// yatarak cerrahi gírisim

        [Display(Name = "rehabilitasyon")]
        Rehabilitation = 6,// rehabilitasyon

        [Display(Name = "konsultasyon")]
        Consultation = 7,// rehabilitasyon

        [Display(Name = "palyatif tedavi")]
        PalliativeTreatment = 8,// Palyatif tedavi

        [Display(Name = "gunubirlik tedavi")]
        DailyTreatment = 9,// gunubirlik tedavi
    }
}
