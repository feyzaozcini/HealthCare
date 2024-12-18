using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.SystemChecks
{
    public class SystemCheckUpdateDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid ProtocolId { get; set; }

        [Required(ErrorMessage ="Bu alan bos birakilamaz")]

        public bool? GeneralSystemCheck { get; set; }

        [Required]
        public bool? GenitoUrinary { get; set; }

        [Required]
        public bool? Skin { get; set; }

        [Required]
        public bool? Respiratory { get; set; }

        [Required]
        public bool? Nervous { get; set; }

        [Required]
        public bool? MusculoSkeletal { get; set; }

        [Required]
        public bool? Circulatory { get; set; }

        [Required]
        public bool? GastroIntestinal { get; set; }
        public string? Description { get; set; }
    }
}
