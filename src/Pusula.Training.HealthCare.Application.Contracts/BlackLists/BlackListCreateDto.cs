using System;
using System.ComponentModel.DataAnnotations;
namespace Pusula.Training.HealthCare.BlackLists
{
    public class BlackListCreateDto
    {
        [Required(ErrorMessage ="Durum Seçilmeli!")]
        public BlackListStatus BlackListStatus { get; set; }

        [Required(ErrorMessage ="Sebebi Kısaca Belirtilmeli")]
        public string Note { get; set; } = null!;

        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
    }
}
