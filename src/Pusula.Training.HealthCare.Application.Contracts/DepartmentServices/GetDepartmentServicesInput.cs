﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.DepartmentServices
{
    public class GetDepartmentServicesInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? Name { get; set; }

        public GetDepartmentServicesInput()
        {
        }
    }
}
