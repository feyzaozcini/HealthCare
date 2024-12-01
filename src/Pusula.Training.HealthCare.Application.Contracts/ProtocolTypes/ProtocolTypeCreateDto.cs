﻿using Pusula.Training.HealthCare.DepartmentServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.ProtocolTypes
{
    public class ProtocolTypeCreateDto
    {
        [Required]
        [StringLength(ProtocolTypeConsts.NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
