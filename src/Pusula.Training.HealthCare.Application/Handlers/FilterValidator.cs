using Pusula.Training.HealthCare.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.Training.HealthCare.Handlers
{
    //public class FilterValidator
    //{
    //    public static GetPatientsInput ValidateFilters(GetPatientsInput filter)
    //    {
    //        // Filtrelerin boş olanlarını null yapma
    //        filter.FirstName = null;
    //        filter.FirstName = string.IsNullOrEmpty(filter.FirstName) ? null : filter.FirstName;
    //        filter.LastName = string.IsNullOrEmpty(filter.LastName) ? null : filter.LastName;
    //        filter.IdentityNumber = string.IsNullOrEmpty(filter.IdentityNumber) ? null : filter.IdentityNumber;
    //        filter.PassportNumber = string.IsNullOrEmpty(filter.PassportNumber) ? null : filter.PassportNumber;
    //        filter.MobilePhoneNumber = string.IsNullOrEmpty(filter.MobilePhoneNumber) ? null : filter.MobilePhoneNumber;
    //        filter.Email = string.IsNullOrEmpty(filter.Email) ? null : filter.Email;
    //        filter.No = filter.No == null ? null : filter.No;

    //        if (filter.FirstName == null && filter.LastName == null && filter.IdentityNumber == null &&
    //        filter.PassportNumber == null && filter.MobilePhoneNumber == null && filter.Email == null &&
    //        filter.No == null)
    //        {
    //            return null; // Boş bir filter döndürüyoruz
    //        }

    //        return filter; 
    //    }
    //}
}
