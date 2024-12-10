using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.PshychologicalStates
{
    public enum MentalState
    {
        [Display(Name = "Sakin")]
        Calm = 1,
        Sad = 2,
        Agitated = 3,//AJITE: Rahatsiz, huzursuz, taskinlik yapan.
        Anxious = 4,
        Aggresive = 5,
    }
}
