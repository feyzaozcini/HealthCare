using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Doctors
{
    public class GetDoctorsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }         
        public Guid? UserId { get; set; }              
        public Guid? TitleId { get; set; }             
        public string? IdentityNumber { get; set; }      
        public DateTime? BirthDateMin { get; set; }     
        public DateTime? BirthDateMax { get; set; }    
        public Gender? Gender { get; set; }            
    }
}
