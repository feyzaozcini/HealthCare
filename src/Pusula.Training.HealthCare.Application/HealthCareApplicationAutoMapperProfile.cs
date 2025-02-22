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
using Pusula.Training.HealthCare.ProtocolTypes;
using Pusula.Training.HealthCare.Notes;
using Pusula.Training.HealthCare.Insurances;
using Pusula.Training.HealthCare.AppointmentRules;
using Pusula.Training.HealthCare.PainTypes;
using Pusula.Training.HealthCare.PainDetails;
using Pusula.Training.HealthCare.DoctorWorkSchedules;
using Pusula.Training.HealthCare.BlackLists;
using Pusula.Training.HealthCare.SystemChecks;
using Pusula.Training.HealthCare.FollowUpPlans;
using Pusula.Training.HealthCare.Addresses;
using Pusula.Training.HealthCare.FamilyHistories;
using Pusula.Training.HealthCare.ControlNotes;
using Pusula.Training.HealthCare.PatientHistories;


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
        CreateMap<Patient, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        CreateMap<PatientWithNavigationProperties, PatientWithNavigationPropertiesDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

        CreateMap<Patient, PatientWithNavigationPropertiesDto>();
        CreateMap<Patient, PatientDeletedDto>();


        CreateMap<Protocol, ProtocolDto>()
            .ForMember(dest => dest.ProtocolTypeName, opt => opt.MapFrom(x => x.ProtocolType.Name))
            .ForMember(dest => dest.ProtocolInsuranceName, opt => opt.MapFrom(x => x.Insurance.Name))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(x => x.Department.Name))
            .ForMember(dest => dest.No, opt => opt.MapFrom(x => x.No))
            .ForMember(dest => dest.PatientFirstName, opt => opt.MapFrom(x => x.Patient.FirstName))
            .ForMember(dest => dest.PatientLastName, opt => opt.MapFrom(x => x.Patient.LastName))
            .ForMember(dest => dest.PatientNo, opt => opt.MapFrom(x => x.Patient.No))
            .ForMember(dest => dest.PatientBirthDate, opt => opt.MapFrom(x => x.Patient.BirthDate))
            .ForMember(dest => dest.PatientGender, opt => opt.MapFrom(x => x.Patient.Gender))
            .ForMember(dest => dest.DoctorTitleName, opt => opt.MapFrom(x => x.Doctor.Title.Name))
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(x => x.Doctor.User.Name))
            .ForMember(dest => dest.DoctorSurname, opt => opt.MapFrom(x => x.Doctor.User.Surname));
        CreateMap<ProtocolDto, ProtocolUpdateDto>();
        CreateMap<ProtocolWithNavigationProperties, ProtocolWithNavigationPropertiesDto>();

        CreateMap<Address, AddressDto>();
        CreateMap<Address, AddressCreateDto>();
        CreateMap<AddressDto, AddressUpdateDto>();
        CreateMap<AddressWithNavigationProperties, AddressWithNavigationPropertiesDto>();

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
            .ForMember(dest => dest.ProtocolStartDate, opt => opt.MapFrom(x => x.Protocol.StartTime))
            .ForMember(dest => dest.ProtocolEndDate, opt => opt.MapFrom(x => x.Protocol.EndTime))
            .ForMember(dest => dest.ProtocolNo, opt => opt.MapFrom(x => x.Protocol.No))
            .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(x => x.Doctor.User.Name))
            .ForMember(dest => dest.DoctorSurname, opt => opt.MapFrom(x => x.Doctor.User.Surname))
            .ForMember(dest => dest.Patient, opt => opt.MapFrom(x => x.Protocol.Patient))
            //.ForMember(dest => dest.PatientSurname, opt => opt.MapFrom(x => x.Protocol.Patient.LastName))
            //.ForMember(dest => dest.PatientMail, opt => opt.MapFrom(x => x.Protocol.Patient.Email))
            //.ForMember(dest => dest.PatientNo, opt => opt.MapFrom(x => x.Protocol.Patient.No))
            //.ForMember(dest => dest.PatientBirthDate, opt => opt.MapFrom(x => x.Protocol.Patient.BirthDate))
            .ForMember(dest => dest.InsuranceName, opt => opt.MapFrom(x => x.Protocol.Insurance.Name))
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
        CreateMap<DoctorWithNavigationPropertiesDto, DepartmentWithDoctorsDto>();
        CreateMap<DoctorWithNavigationProperties, DoctorWithNavigationPropertiesDto>();

        CreateMap<Doctor, DoctorDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
            .ForMember(dest => dest.SurName, opt => opt.MapFrom(src => src.User.Surname))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.TitleName, opt => opt.MapFrom(src => src.Title.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
            .ForMember(dest => dest.DoctorDepartments,
               opt => opt.MapFrom(src => src.DoctorDepartments.Select(dd => dd.DepartmentId).ToList()));


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
        CreateMap<AppointmentType, AppointmentTypeUpdateDto>()
            .ForMember(dest => dest.DoctorIds, opt => opt.MapFrom(src => src.DoctorAppointmentTypes.Select(dd => dd.DoctorId).ToList()));

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

        CreateMap<Appointment, AppointmentDto>();
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
        CreateMap<Diagnosis, LookupDto<Guid>>().ForMember(dest => dest.DisplayName,opt => opt.MapFrom(src => $"{src.Code} - {src.Name}"));


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
            .ForMember(dest => dest.TestGroupItemName, opt => opt.MapFrom(x => x.TestGroupItem.Name))
            .ForMember(dest => dest.TestGroupItemCode, opt => opt.MapFrom(x => x.TestGroupItem.Code))
            .ForMember(dest => dest.TestGroupItemTurnaroundTime, opt => opt.MapFrom(x => x.TestGroupItem.TurnaroundTime))
            .ForMember(dest => dest.TestGroupItemDescription, opt => opt.MapFrom(x => x.TestGroupItem.Description))
            .ForMember(dest => dest.TestGroupName, opt => opt.MapFrom(x => x.TestGroupItem.TestGroup.Name))
            .ReverseMap();

        CreateMap<TestValueRange, TestValueRangesCreateDto>().ReverseMap();
        CreateMap<TestValueRange, TestValueRangesUpdateDto>().ReverseMap();
        CreateMap<TestValueRange, TestValueRangesDeletedDto>().ReverseMap();
        CreateMap<TestValueRange, GetTestValueRangesInput>().ReverseMap();


        CreateMap<TestProcessWithNavigationProperties, TestProcessWithNavigationPropertiesDto>()
            .ForMember(dest => dest.TestProcess, opt => opt.MapFrom(src => src.TestProcess))
            .ForMember(dest => dest.LabRequest, opt => opt.MapFrom(src => src.LabRequest))
            .ForMember(dest => dest.TestGroupItem, opt => opt.MapFrom(src => src.TestGroupItem))
            .ForMember(dest => dest.TestValueRange, opt => opt.MapFrom(src => src.TestValueRange));
        CreateMap<TestProcess, TestProcessDto>().ReverseMap();
        CreateMap<TestProcess, TestProcessesCreateDto>().ReverseMap();
        CreateMap<TestProcessDto, TestProcessesCreateDto>().ReverseMap();
        CreateMap<TestProcess, TestProcessesUpdateDto>().ReverseMap();
        CreateMap<TestProcess, TestProcessesDeletedDto>().ReverseMap();
        CreateMap<TestProcess, GetTestProcessesInput>().ReverseMap();
        CreateMap<TestProcess, TestProcessDto>().ReverseMap();

        CreateMap<ProtocolType, ProtocolTypeDto>();
        CreateMap<ProtocolTypeDto, ProtocolTypeUpdateDto>();
        CreateMap<ProtocolType, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<Note, NoteDto>();
        CreateMap<NoteDto, NoteUpdateDto>();
        CreateMap<Note, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Text));

        CreateMap<Insurance, InsuranceDto>();
        CreateMap<InsuranceDto, InsuranceUpdateDto>();
        CreateMap<Insurance, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<AppointmentRule, AppointmentRuleDto>();
        CreateMap<AppointmentRule, AppointmentRuleWithNavigationPropertiesDto>();
        CreateMap<AppointmentRuleWithNavigationProperties, AppointmentRuleWithNavigationPropertiesDto>();
        CreateMap<AppointmentRuleDto, AppointmentRuleWithNavigationPropertiesDto>();

        CreateMap<PainType, PainTypeDto>();
        CreateMap<PainTypeDto, PainTypeUpdateDto>();
        CreateMap<PainType, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<PainDetail, PainDetailDto>()
           .ForMember(dest => dest.PainTypeName, opt => opt.MapFrom(x => x.PainType.Name))
           .ReverseMap();


        CreateMap<DoctorWorkSchedule, DoctorWorkScheduleDto>();
        CreateMap<DoctorWorkSchedule, DoctorWorkScheduleWithNavigationPropertiesDto>();
        CreateMap<DoctorWorkScheduleWithNavigationProperties, DoctorWorkScheduleWithNavigationPropertiesDto>();
        CreateMap<DoctorWorkScheduleDto, DoctorWorkScheduleWithNavigationPropertiesDto>();


        CreateMap<BlackList, BlackListDto>();
        CreateMap<BlackList, BlackListWithNavigationPropertiesDto>();
        CreateMap<BlackListWithNavigationProperties, BlackListWithNavigationPropertiesDto>();
        CreateMap<BlackListDto, BlackListWithNavigationPropertiesDto>();

        CreateMap<SystemCheck, SystemCheckDto>().ReverseMap();
        CreateMap<FollowUpPlan, FollowUpPlanDto>().ReverseMap();
        CreateMap<FamilyHistory, FamilyHistoryDto>().ReverseMap();

        CreateMap<ControlNote, GetControlsInput>().ReverseMap();
        CreateMap<ControlNote, ControlNoteDto>()
            .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(x => x.User.Name))
            .ForMember(dest => dest.CreatorSurname, opt => opt.MapFrom(x => x.User.Surname))
            .ForMember(dest => dest.CreatorTitle, opt => opt.MapFrom(x =>x.CreatorId == x.Protocol.Doctor.UserId? x.Protocol.Doctor.Title.Name: null));

        CreateMap<PatientHistory, PatientHistoryDto>().ReverseMap();




    }
}
