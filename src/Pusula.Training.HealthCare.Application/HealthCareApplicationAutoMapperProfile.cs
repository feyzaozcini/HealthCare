using AutoMapper;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.AppointmentTypes;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.DepartmentServices;
using Pusula.Training.HealthCare.DoctorDepartments;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.LabRequests;
using Pusula.Training.HealthCare.PatientCompanies;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.TestGroupItems;
using Pusula.Training.HealthCare.TestGroups;
using Pusula.Training.HealthCare.Titles;
using System;
using Volo.Abp.Identity;
using Pusula.Training.HealthCare.Districts;
using Pusula.Training.HealthCare.Villages;
using Pusula.Training.HealthCare.Appointments;

namespace Pusula.Training.HealthCare;

public class HealthCareApplicationAutoMapperProfile : Profile
{
    public HealthCareApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Patient, PatientDto>();
        CreateMap<Patient, PatientExcelDto>();
        CreateMap<PatientDto, PatientUpdateDto>();
        CreateMap<Patient, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.FirstName));
        CreateMap<PatientWithNavigationProperties, PatientWithNavigationPropertiesDto>();
        CreateMap<Patient, PatientWithNavigationPropertiesDto>();
        CreateMap<Patient, PatientDeletedDto>();

        CreateMap<Protocol, ProtocolDto>();
        CreateMap<Protocol, ProtocolExcelDto>();
        CreateMap<ProtocolDto, ProtocolUpdateDto>();
        CreateMap<ProtocolWithNavigationProperties, ProtocolWithNavigationPropertiesDto>();

        CreateMap<Department, DepartmentDto>();
        CreateMap<Department, DepartmentExcelDto>();
        CreateMap<DepartmentDto, DepartmentUpdateDto>();
        CreateMap<Department, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<PatientCompany, PatientCompanyDto>();
        CreateMap<PatientCompany, PatientCompanyExcelDto>();
        CreateMap<PatientCompanyDto, PatientCompanyUpdateDto>();
        CreateMap<PatientCompany, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));
        CreateMap<PatientCompany, PatientCompanyDeleteDto>();

        CreateMap<LabRequest, GetLabRequestsInput>().ReverseMap();
        CreateMap<LabRequest, LabRequestCreateDto>().ReverseMap();
        CreateMap<LabRequest, LabRequestUpdateDto>().ReverseMap();
        CreateMap<LabRequest, LabRequestDeletedDto>().ReverseMap();
        CreateMap<LabRequest, LabRequestDto>().ReverseMap();

        CreateMap<TestGroup, GetTestGroupsInput>().ReverseMap();
        CreateMap<TestGroup, GetTestGroupItemsInput>();
        CreateMap<TestGroup, TestGroupsCreateDto>().ReverseMap();
        CreateMap<TestGroup, TestGroupsUpdateDto>().ReverseMap();
        CreateMap<TestGroup, TestGroupsDeletedDto>().ReverseMap();
        CreateMap<TestGroup, TestGroupDto>().ReverseMap();
        CreateMap<TestGroup, LookupDto<Guid>>()
    .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<TestGroupItem, GetTestGroupItemsInput>().ReverseMap();
        CreateMap<TestGroupItem, TestGroupItemsCreateDto>().ReverseMap();
        CreateMap<TestGroupItem, TestGroupItemsUpdateDto>().ReverseMap();
        CreateMap<TestGroupItem, TestGroupItemsDeletedDto>().ReverseMap();
        CreateMap<TestGroupItem, TestGroupItemDto>().ReverseMap();
        CreateMap<TestGroupItemWithNavigationProperties, TestGroupItemWithNavigationPropertiesDto>();

        CreateMap<DepartmentService, DepartmentServiceDto>();
        CreateMap<DepartmentService, DepartmentServiceExcelDto>();
        CreateMap<DepartmentServiceDto, DepartmentServiceUpdateDto>();
        CreateMap<DepartmentService, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<Country, CountryDto>();
        CreateMap<Country, CountryCreateDto>();
        CreateMap<Country, CountryDeletedDto>();
        CreateMap<CountryDto, CountryCreateDto>();
        CreateMap<Country, CountryUpdateDto>();
        CreateMap<CountryDto, CountryUpdateDto>();
        CreateMap<CountryDto, CountryDeletedDto>();
        CreateMap<Country, CountryExcelDto>();
        CreateMap<Country, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));
        CreateMap<Country, GetCountryLookupDto<Guid>>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                                                 .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code));

        CreateMap<City, CityDto>();
        CreateMap<City, CityCreateDto>();
        CreateMap<City, CityDeleteDto>();
        CreateMap<CityDto, CityCreateDto>();
        CreateMap<City, CityUpdateDto>();
        CreateMap<CityDto, CityUpdateDto>();
        CreateMap<CityDto, CityDeleteDto>();
        CreateMap<City, CityExcelDto>();
        CreateMap<City, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<Doctor, DoctorDto>();
        CreateMap<IdentityUser, DoctorDto>()
           .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
        CreateMap<DoctorWithNavigationProperties, DoctorDto>()
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
        .ForMember(dest => dest.SurName, opt => opt.MapFrom(src => src.User.Surname))
        .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
        .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
        .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
        .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
        .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.Doctor.BirthDate))
        .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Doctor.Gender));
        CreateMap<Doctor, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.UserId.ToString()));


        CreateMap<AppointmentType, GetAppointmentTypesInput>();
        CreateMap<AppointmentType, AppointmentTypeCreateDto>();
        CreateMap<AppointmentType, AppointmentTypeUpdateDto>();
        CreateMap<AppointmentType, AppointmentTypeDeleteDto>();
        CreateMap<AppointmentType, AppointmentTypeDto>();
        CreateMap<AppointmentType, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<Title, TitleDto>();
        CreateMap<Title, TitleExcelDto>();
        CreateMap<TitleDto, TitleUpdateDto>();
        CreateMap<Title, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<District, DistrictDto>();
        CreateMap<District, DistrictCreateDto>();
        CreateMap<District, DistrictDeleteDto>();
        CreateMap<DistrictDto, DistrictCreateDto>();
        CreateMap<District, DistrictUpdateDto>();
        CreateMap<DistrictDto, DistrictUpdateDto>();
        CreateMap<DistrictDto, DistrictDeleteDto>();
        CreateMap<District, DistrictExcelDto>();
        CreateMap<District, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<Village, VillageDto>();
        CreateMap<Village, VillageCreateDto>();
        CreateMap<Village, VillageDeleteDto>();
        CreateMap<VillageDto, VillageCreateDto>();
        CreateMap<Village, VillageUpdateDto>();
        CreateMap<VillageDto, VillageUpdateDto>();
        CreateMap<VillageDto, VillageDeleteDto>();
        CreateMap<Village, VillageExcelDto>();
        CreateMap<Village, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<Appointment,AppointmentDto>();
        CreateMap<Appointment, AppointmentWithNavigationPropertiesDto>();
        CreateMap<AppointmentWithNavigationProperties, AppointmentWithNavigationPropertiesDto>();


    }
}
