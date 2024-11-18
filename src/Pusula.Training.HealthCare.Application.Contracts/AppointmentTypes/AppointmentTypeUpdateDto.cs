﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.AppointmentTypes
{
    public class AppointmentTypeUpdateDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
    }
}
