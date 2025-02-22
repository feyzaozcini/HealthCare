﻿using Pusula.Training.HealthCare.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Anamneses
{
    public class AnamnesisWithNavigationProperties
    {
        public Anamnesis Anamnesis { get; set; } = null!;

        public Protocol Protocol { get; set; } = null!;
    }
}
