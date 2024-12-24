using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Anamneses
{
    public class AnamnesisCreateDto
    {

        [Required(ErrorMessage = "Şikayet alanı zorunludur.")]
        public string Complaint { get; set; } = null!;

        [Required(ErrorMessage = "Başlangıç tarihi zorunludur.")]

        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Öykü alanı zorunludur.")]
        public string Story { get; set; } = null!;

        [Required]
        public Guid ProtocolId { get; set; }
    }
}
