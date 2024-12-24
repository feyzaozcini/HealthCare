﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.FamilyHistories
{
    public class FamilyHistoryDto :FullAuditedEntityDto<Guid>
    {
        [Required]
        public Guid PatientId { get;  set; }

        public string? Mother { get;  set; }

        public string? Father { get;  set; }

        public string? Sister { get;  set; }

        public string? Brother { get;  set; }

        public string? Other { get;  set; }

        [Required]
        public bool IsParentsRelative { get;  set; }
    }
}
