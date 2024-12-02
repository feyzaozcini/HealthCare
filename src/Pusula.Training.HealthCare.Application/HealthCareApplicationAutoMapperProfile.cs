﻿using AutoMapper;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.AppointmentTypes;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.DepartmentServices;
using Pusula.Training.HealthCare.DoctorDepartments;
using Pusula.Training.HealthCare.DiagnosisGroups;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.LabRequests;
using Pusula.Training.HealthCare.PatientCompanies;
using Pusula.Training.HealthCare.Patients;
using Pusula.Training.HealthCare.Protocols;
using Pusula.Training.HealthCare.Shared;
using Pusula.Training.HealthCare.TestGroupItems;
using Pusula.Training.HealthCare.TestGroups;
using Pusula.Training.HealthCare.Titles;
using Pusula.Training.HealthCare.Diagnoses;
using System;
using Volo.Abp.Identity;
using Pusula.Training.HealthCare.Anamneses;
using Pusula.Training.HealthCare.ExaminationDiagnoses;
using Pusula.Training.HealthCare.FallRisks;
using Pusula.Training.HealthCare.PhysicalExaminations;
using Pusula.Training.HealthCare.PshychologicalStates;
using Pusula.Training.HealthCare.Districts;
using Pusula.Training.HealthCare.Villages;
using Pusula.Training.HealthCare.Appointments;
using System.Linq;
using Pusula.Training.HealthCare.TestValueRanges;
using Pusula.Training.HealthCare.TestProcesses;

using Pusula.Training.HealthCare.DoctorAppoinmentTypes;
using Pusula.Training.HealthCare.ProtocolTypes;
using Pusula.Training.HealthCare.Notes;
using Pusula.Training.HealthCare.Insurances;


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
        CreateMap<Department, DepartmentWithDoctorsDto>();
        CreateMap<Department, DepartmentWithDoctorsDto>()
            .ForMember(dest => dest.Doctors,
                       opt => opt.MapFrom(src => src.Doctors));

        CreateMap<Department, DepartmentWithDoctorsDto>();
        CreateMap<Department, DepartmentDto>()
            .ForMember(dest => dest.DoctorDepartments, opt => opt.MapFrom(src => src.Doctors.Select(dd => dd.DoctorId).ToList()));

        CreateMap<Department, DepartmentWithDoctorsDto>()
            .ForMember(dest => dest.Doctors, opt => opt.MapFrom(src => src.Doctors.Select(dd => dd.Doctor).ToList()));

        CreateMap<PatientCompany, PatientCompanyDto>();
        CreateMap<PatientCompany, PatientCompanyExcelDto>();
        CreateMap<PatientCompanyDto, PatientCompanyUpdateDto>();
        CreateMap<PatientCompany, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));
        CreateMap<PatientCompany, PatientCompanyDeleteDto>();

        CreateMap<LabRequest, GetLabRequestsInput>().ReverseMap();
        CreateMap<LabRequest, LabRequestCreateDto>().ReverseMap();
        CreateMap<LabRequest, LabRequestUpdateDto>().ReverseMap();
        CreateMap<LabRequest, LabRequestDeletedDto>().ReverseMap();
        CreateMap<LabRequest, LabRequestDto>()
            .ForMember(dest => dest.ProtocolType, opt=> opt.MapFrom(x => x.Protocol.Type))
            .ForMember(dest => dest.ProtocolStartDate, opt=> opt.MapFrom(x => x.Protocol.StartTime))
            .ForMember(dest => dest.ProtocolEndDate, opt=> opt.MapFrom(x => x.Protocol.EndTime))
            .ForMember(dest => dest.DoctorName, opt=> opt.MapFrom(x => x.Doctor.User.Name))
            .ForMember(dest => dest.DoctorSurname, opt=> opt.MapFrom(x => x.Doctor.User.Surname))
            //.ForMember(dest => dest.PatientName, opt=> opt.MapFrom(x => x.Protocol.Patient.FirstName))
            //.ForMember(dest => dest.PatientSurname, opt=> opt.MapFrom(x => x.Protocol.Patient.LastName))
            //.ForMember(dest => dest.PatientNo, opt=> opt.MapFrom(x => x.Protocol.Patient.No))
            .ReverseMap();

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
        CreateMap<TestGroupItem, TestGroupItemDto>()
            .ForMember(dest => dest.TestGroupName, opt => opt.MapFrom(x => x.TestGroup.Name))
            .ReverseMap();
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

        
        CreateMap<IdentityUser, DoctorDto>()
         .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
         .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<DoctorWithNavigationProperties, DoctorDto>()
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
        .ForMember(dest => dest.SurName, opt => opt.MapFrom(src => src.User.Surname))
        .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
        .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
        .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
        .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
        .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.Doctor.BirthDate))
        .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Doctor.Gender))
        .ForMember(dest => dest.TitleId, opt => opt.MapFrom(src => src.Doctor.TitleId))
        .ForMember(dest => dest.DoctorDepartments, opt => opt.MapFrom(src => src.DoctorDepartments.Select(dd => dd.DepartmentId).ToList()));
        CreateMap<Doctor, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.UserId.ToString()));
    //    CreateMap<Doctor, DoctorDto>()
    //.ForMember(dest => dest.DoctorDepartments,
    //           opt => opt.MapFrom(src => src.DoctorDepartments.Select(dd => dd.DepartmentId).ToList()));
        CreateMap<DepartmentWithDoctorsDto, DoctorWithNavigationPropertiesDto>();
        CreateMap<DoctorWithNavigationPropertiesDto,DepartmentWithDoctorsDto>();
        CreateMap<DoctorWithNavigationProperties, DoctorWithNavigationPropertiesDto>();

        CreateMap<Doctor, DoctorDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
            .ForMember(dest => dest.SurName, opt => opt.MapFrom(src => src.User.Surname))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
            .ForMember(dest => dest.DoctorDepartments,
               opt => opt.MapFrom(src => src.DoctorDepartments.Select(dd => dd.DepartmentId).ToList()))
            .ForMember(dest => dest.TitleName, opt => opt.MapFrom(src => src.Title.Name));

       
        CreateMap<DoctorCreateDto, Doctor>();
        CreateMap<DoctorWithNavigationProperties, DoctorWithNavigationPropertiesDto>()
   .ForMember(dest => dest.Doctor, opt => opt.MapFrom(src => src.Doctor))
   .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
   .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
        CreateMap<Doctor, DoctorWithDepartmentDto>()
    .ForMember(dest => dest.Departments, opt => opt.MapFrom(src => src.DoctorDepartments.Select(dd => dd.Department)));
        CreateMap<Doctor, DoctorWithNavigationPropertiesDto>()
            .ForMember(dest => dest.Doctor, opt => opt.MapFrom(src => src));

        CreateMap<AppointmentType, GetAppointmentTypesInput>();
        CreateMap<AppointmentType, AppointmentTypeCreateDto>();
        CreateMap<AppointmentType, AppointmentTypeUpdateDto>();
        CreateMap<AppointmentType, AppointmentTypeDeleteDto>();
        CreateMap<AppointmentType, AppointmentTypeDto>();
        CreateMap<AppointmentType, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));
        CreateMap<AppointmentType, AppointmentTypeDto>()
            .ForMember(dest => dest.DoctorAppointmentTypes, opt => opt.MapFrom(src => src.DoctorAppointmentTypes.Select(dd => dd.DoctorId).ToList()));
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
        CreateMap<IdentityUser, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<Appointment,AppointmentDto>();
        CreateMap<Appointment, AppointmentWithNavigationPropertiesDto>();
        CreateMap<AppointmentWithNavigationProperties, AppointmentWithNavigationPropertiesDto>();
        CreateMap<AppointmentDto, AppointmentWithNavigationPropertiesDto>();


        CreateMap<DiagnosisGroup, DiagnosisGroupDto>().ReverseMap();
        CreateMap<DiagnosisGroupCreateDto, DiagnosisGroup>().ReverseMap();
        CreateMap<DiagnosisGroupUpdateDto, DiagnosisGroup>().ReverseMap();
        CreateMap<DiagnosisGroupDto, DiagnosisGroupUpdateDto>().ReverseMap();


        CreateMap<Diagnosis, DiagnosisDto>().ReverseMap();
        CreateMap<DiagnosisCreateDto, Diagnosis>().ReverseMap();
        CreateMap<DiagnosisUpdateDto, Diagnosis>().ReverseMap();
        CreateMap<DiagnosisDto, DiagnosisUpdateDto>().ReverseMap();
        CreateMap<DiagnosisWithNavigationProperties, DiagnosisWithNavigationPropertiesDto>().ReverseMap();


        CreateMap<Anamnesis, AnamnesisDto>().ReverseMap();
        CreateMap<AnamnesisCreateDto, Anamnesis>().ReverseMap();
        CreateMap<AnamnesisUpdateDto, Anamnesis>().ReverseMap();
        CreateMap<AnamnesisWithNavigationProperties, AnamnesisWithNavigationPropertiesDto>().ReverseMap();

        CreateMap<ExaminationDiagnosis, ExaminationDiagnosisDto>().ReverseMap();
        CreateMap<ExaminationDiagnosisCreateDto, ExaminationDiagnosis>().ReverseMap();
        CreateMap<ExaminationDiagnosisUpdateDto, ExaminationDiagnosis>().ReverseMap();
        CreateMap<ExaminationDiagnosisWithNavigationProperties, ExaminationDiagnosisWithNavigationPropertiesDto>().ReverseMap();

        CreateMap<FallRisk, FallRiskDto>().ReverseMap();
        CreateMap<FallRiskCreateDto, FallRisk>().ReverseMap();
        CreateMap<FallRiskUpdateDto, FallRisk>().ReverseMap();
        CreateMap<FallRiskWithNavigationProperties, FallRiskWithNavigationPropertiesDto>().ReverseMap();

        CreateMap<PhysicalExamination, PhysicalExaminationDto>().ReverseMap();
        CreateMap<PhysicalExaminationCreateDto, PhysicalExamination>().ReverseMap();
        CreateMap<PhysicalExaminationUpdateDto, PhysicalExamination>().ReverseMap();
        CreateMap<PhysicalExaminationWithNavigationProperties, PhysicalExaminationWithNavigationPropertiesDto>().ReverseMap();

        CreateMap<PshychologicalState, PshychologicalStateDto>().ReverseMap();
        CreateMap<PshychologicalStateCreateDto, PshychologicalState>().ReverseMap();
        CreateMap<PshychologicalStateUpdateDto, PshychologicalState>().ReverseMap();
        CreateMap<PshychologicalStateWithNavigationDto, PshychologicalStateWithNavigationProperties>().ReverseMap();

        CreateMap<TestValueRange, TestValueRangeDto>()
            .ForMember(dest => dest.TestGroupItemName, opt => opt.MapFrom(x=> x.TestGroupItem.Name))
            .ForMember(dest => dest.TestGroupItemCode, opt => opt.MapFrom(x=> x.TestGroupItem.Code))
            .ForMember(dest => dest.TestGroupItemTurnaroundTime, opt => opt.MapFrom(x=> x.TestGroupItem.TurnaroundTime))
            .ForMember(dest => dest.TestGroupItemDescription, opt => opt.MapFrom(x=> x.TestGroupItem.Description))
            .ForMember(dest => dest.TestGroupName, opt => opt.MapFrom(x=> x.TestGroupItem.TestGroup.Name))
            .ReverseMap();

        CreateMap<TestValueRange, TestValueRangesCreateDto>().ReverseMap();
        CreateMap<TestValueRange, TestValueRangesUpdateDto>().ReverseMap();
        CreateMap<TestValueRange, TestValueRangesDeletedDto>().ReverseMap();
        CreateMap<TestValueRange, GetTestValueRangesInput>().ReverseMap();

        CreateMap<TestProcess, TestProcessDto>().ReverseMap();
        CreateMap<TestProcess, TestProcessesCreateDto>().ReverseMap();
        CreateMap<TestProcess, TestProcessesUpdateDto>().ReverseMap();
        CreateMap<TestProcess, TestProcessesDeletedDto>().ReverseMap();
        CreateMap<TestProcess, GetTestProcessesInput>().ReverseMap();
        CreateMap<TestProcess, GetTestProcessWithNavigationPropertiesDto>()
            .ForMember(dest => dest.LabRequestCreatedDate, opt=> opt.MapFrom(x=> x.LabRequest.CreationTime))
            .ForMember(dest => dest.LabRequestStatus, opt=> opt.MapFrom(x=> x.LabRequest.Status))
            .ForMember(dest => dest.LabRequestDescription, opt=> opt.MapFrom(x=> x.LabRequest.Description))
            .ForMember(dest => dest.TestGroupItemName, opt=> opt.MapFrom(x=> x.TestGroupItem.Name))
            .ForMember(dest => dest.TestGroupItemCode, opt=> opt.MapFrom(x=> x.TestGroupItem.Code))
            .ForMember(dest => dest.TestGroupItemType, opt=> opt.MapFrom(x=> x.TestGroupItem.TestType))
            .ForMember(dest => dest.TestGroupItemDescription, opt=> opt.MapFrom(x=> x.TestGroupItem.Description))
            .ForMember(dest => dest.TestGroupItemTurnaroundTime, opt=> opt.MapFrom(x=> x.TestGroupItem.TurnaroundTime))
            .ReverseMap();

        CreateMap<ProtocolType, ProtocolTypeDto>();
        CreateMap<ProtocolTypeDto, ProtocolTypeUpdateDto>();
        CreateMap<ProtocolType, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<Note, NoteDto>();
        CreateMap<NoteDto, NoteUpdateDto>();
        CreateMap<Note, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Text));

        CreateMap<Insurance, InsuranceDto>();
        CreateMap<InsuranceDto, InsuranceUpdateDto>();
        CreateMap<Insurance, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));
    }
}
