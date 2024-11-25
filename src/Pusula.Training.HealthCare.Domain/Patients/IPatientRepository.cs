using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Pusula.Training.HealthCare.Patients;

public interface IPatientRepository : IRepository<Patient, Guid>
{

    Task DeleteAllAsync(
        string? filterText = null,
        string? firstName = null,
        string? lastName = null,
        DateTime? birthDateMin = null,
        DateTime? birthDateMax = null,
        string? identityNumber = null,
        string? passportNumber = null,
        string? email = null,
        string? mobilePhoneNumber = null,
        string? emergencyPhoneNumber = null,
        Gender? gender = null,
        int? no=null,
        string? motherName = null,
        string? fatherName = null,
        BloodType? bloodType = null,
        Type? type = null,
        Guid? companyId = null,
        Guid? primaryCountryId = null, 
        Guid? primaryCityId = null, 
        Guid? primaryDistrictId = null, 
        Guid? primaryVillageId = null,
        string? primaryAddressDescription = null,
        Guid? secondaryCountryId = null, 
        Guid? secondaryCityId = null, 
        Guid? secondaryDistrictId = null, 
        Guid? secondaryVillageId = null,
        string? secondaryAddressDescription = null,
        CancellationToken cancellationToken = default);

    
     Task<PatientWithNavigationProperties> GetWithNavigationPropertiesAsync(
         Guid id,
         CancellationToken cancellationToken = default);

    Task<List<PatientWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
        string? filterText = null,
        string? firstName = null,
        string? lastName = null,
        DateTime? birthDateMin = null,
        DateTime? birthDateMax = null,
        string? identityNumber = null,
        string? passportNumber = null,
        string? email = null,
        string? mobilePhoneNumber = null,
        string? emergencyPhoneNumber = null,
        Gender? gender = null,
        int? no = null,
        string? motherName = null,
        string? fatherName = null,
        BloodType? bloodType = null,
        Type? type = null,
        Guid? companyId = null,
        Guid? primaryCountryId = null, 
        Guid? primaryCityId = null, 
        Guid? primaryDistrictId = null, 
        Guid? primaryVillageId = null,
        string? primaryAddressDescription = null,
        Guid? secondaryCountryId = null, 
        Guid? secondaryCityId = null, 
        Guid? secondaryDistrictId = null, 
        Guid? secondaryVillageId = null,
        string? secondaryAddressDescription = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
        );

    Task<List<Patient>> GetListAsync(
        string? filterText = null,
        string? firstName = null,
        string? lastName = null,
        DateTime? birthDateMin = null,
        DateTime? birthDateMax = null,
        string? identityNumber = null,
        string? passportNumber = null,
        string? email = null,
        string? mobilePhoneNumber = null,
        string? emergencyPhoneNumber = null,
        Gender? gender = null,
        int? no = null,
        string? motherName = null,
        string? fatherName = null,
        BloodType? bloodType = null,
        Type? type = null,
        Guid? companyId = null,
        Guid? primaryCountryId = null, 
        Guid? primaryCityId = null, 
        Guid? primaryDistrictId = null, 
        Guid? primaryVillageId = null,
        string? primaryAddressDescription = null,
        Guid? secondaryCountryId = null, 
        Guid? secondaryCityId = null, 
        Guid? secondaryDistrictId = null, 
        Guid? secondaryVillageId = null,
        string? secondaryAddressDescription = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
            );

    Task<long> GetCountAsync(
        string? filterText = null,
        string? firstName = null,
        string? lastName = null,
        DateTime? birthDateMin = null,
        DateTime? birthDateMax = null,
        string? identityNumber = null,
        string? passportNumber = null,
        string? email = null,
        string? mobilePhoneNumber = null,
        string? emergencyPhoneNumber = null,
        Gender? gender = null,
        int? no = null,
        string? motherName = null,
        string? fatherName = null,
        BloodType? bloodType = null,
        Type? type = null,
        Guid? companyId = null,
        Guid? primaryCountryId = null, 
        Guid? primaryCityId = null, 
        Guid? primaryDistrictId = null,
        Guid? primaryVillageId = null,
        string? primaryAddressDescription = null,
        Guid? secondaryCountryId = null, 
        Guid? secondaryCityId = null, 
        Guid? secondaryDistrictId = null, 
        Guid? secondaryVillageId = null,
        string? secondaryAddressDescription = null,
        string? sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default);
}