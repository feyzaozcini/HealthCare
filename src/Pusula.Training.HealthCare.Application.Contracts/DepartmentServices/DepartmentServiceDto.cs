﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Pusula.Training.HealthCare.DepartmentServices
{
    public class DepartmentServiceDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Name { get; set; } = null!;

        public string ConcurrencyStamp { get; set; } = null!;

    }
}
