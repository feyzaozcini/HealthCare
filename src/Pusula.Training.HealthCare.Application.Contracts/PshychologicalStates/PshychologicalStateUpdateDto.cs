using System;
using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.PshychologicalStates
{
    public class PshychologicalStateUpdateDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "psikolojik durum seçimi boş bırakılamaz.")]
        public MentalState MentalState { get; set; }
        public Guid ProtocolId { get; set; }
    }
}
