using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Cities
{
    public class GetCityLookupDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
