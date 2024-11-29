using Microsoft.VisualBasic;
using Pusula.Training.HealthCare.AppointmentTypes;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Departments;
using Pusula.Training.HealthCare.Districts;
using Pusula.Training.HealthCare.Doctors;
using Pusula.Training.HealthCare.PatientCompanies;
using Pusula.Training.HealthCare.TestGroupItems;
using Pusula.Training.HealthCare.TestGroups;
using Pusula.Training.HealthCare.Titles;
using Pusula.Training.HealthCare.Villages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;


namespace Pusula.Training.HealthCare
{
    public class HealthCareDataSeederContributor(
        ICountryRepository countryRepository,
        ICityRepository cityRepository,
        IDistrictRepository districtRepository,
        IVillageRepository villageRepository,
        ITestGroupRepository testGroupRepository,
        ITestGroupItemRepository testGroupItemRepository,
        IDepartmentRepository departmentRepository,
        ITitleRepository titleRepository,
        IAppointmentTypeRepository appointmentTypeRepository,
        IPatientCompanyRepository patientCompanyRepository,
        IGuidGenerator guidGenerator) : IDataSeedContributor, ITransientDependency
    {
        public async Task SeedAsync(DataSeedContext context)
        {
            // Eğer veritabanında Country verisi varsa, işlemi sonlandırıyoruz.
            //if (await countryRepository.GetCountAsync() > 0) return;

            // Seed işlemleri
            var countries = await SeedCountriesAsync();
            var cities = await SeedCitiesAsync(countries);
            var districts = await SeedDistrictsAsync(cities);
            await SeedVillagesAsync(districts);

            var testGroups = await SeedTestGroupsAsync();
            var testGroupItems = await SeedTestGroupItemsAsync(testGroups);

            var departments = await SeedDepartmentsAsync();
            var titles = await SeedTitlesAsync();
            //var appointmentTypes = await SeedAppointmentTypesAsync();
            var patientCompanies = await SeedPatientCompaniesAsync();
        }

        #region Countries
        private async Task<List<Guid>> SeedCountriesAsync()
        {
            if (await countryRepository.GetCountAsync() > 0)
                return new List<Guid>();

            var countries = new List<Country>
    {
        new Country(guidGenerator.Create(), "Afghanistan", "+93"),
        new Country(guidGenerator.Create(), "Albania", "+355"),
        new Country(guidGenerator.Create(), "Algeria", "+213"),
        new Country(guidGenerator.Create(), "Andorra", "+376"),
        new Country(guidGenerator.Create(), "Angola", "+244"),
        new Country(guidGenerator.Create(), "Antigua and Barbuda", "+1-268"),
        new Country(guidGenerator.Create(), "Argentina", "+54"),
        new Country(guidGenerator.Create(), "Armenia", "+374"),
        new Country(guidGenerator.Create(), "Australia", "+61"),
        new Country(guidGenerator.Create(), "Austria", "+43"),
        new Country(guidGenerator.Create(), "Azerbaijan", "+994"),
        new Country(guidGenerator.Create(), "Bahamas", "+1-242"),
        new Country(guidGenerator.Create(), "Bahrain", "+973"),
        new Country(guidGenerator.Create(), "Bangladesh", "+880"),
        new Country(guidGenerator.Create(), "Barbados", "+1-246"),
        new Country(guidGenerator.Create(), "Belarus", "+375"),
        new Country(guidGenerator.Create(), "Belgium", "+32"),
        new Country(guidGenerator.Create(), "Belize", "+501"),
        new Country(guidGenerator.Create(), "Benin", "+229"),
        new Country(guidGenerator.Create(), "Bhutan", "+975"),
        new Country(guidGenerator.Create(), "Bolivia", "+591"),
        new Country(guidGenerator.Create(), "Bosnia and Herzegovina", "+387"),
        new Country(guidGenerator.Create(), "Botswana", "+267"),
        new Country(guidGenerator.Create(), "Brazil", "+55"),
        new Country(guidGenerator.Create(), "Brunei", "+673"),
        new Country(guidGenerator.Create(), "Bulgaria", "+359"),
        new Country(guidGenerator.Create(), "Burkina Faso", "+226"),
        new Country(guidGenerator.Create(), "Burundi", "+257"),
        new Country(guidGenerator.Create(), "Cabo Verde", "+238"),
        new Country(guidGenerator.Create(), "Cambodia", "+855"),
        new Country(guidGenerator.Create(), "Cameroon", "+237"),
        new Country(guidGenerator.Create(), "Canada", "+1"),
        new Country(guidGenerator.Create(), "Central African Republic", "+236"),
        new Country(guidGenerator.Create(), "Chad", "+235"),
        new Country(guidGenerator.Create(), "Chile", "+56"),
        new Country(guidGenerator.Create(), "China", "+86"),
        new Country(guidGenerator.Create(), "Colombia", "+57"),
        new Country(guidGenerator.Create(), "Comoros", "+269"),
        new Country(guidGenerator.Create(), "Congo (Congo-Brazzaville)", "+242"),
        new Country(guidGenerator.Create(), "Congo (Congo-Kinshasa)", "+243"),
        new Country(guidGenerator.Create(), "Costa Rica", "+506"),
        new Country(guidGenerator.Create(), "Croatia", "+385"),
        new Country(guidGenerator.Create(), "Cuba", "+53"),
        new Country(guidGenerator.Create(), "Cyprus", "+357"),
        new Country(guidGenerator.Create(), "Czechia (Czech Republic)", "+420"),
        new Country(guidGenerator.Create(), "Denmark", "+45"),
        new Country(guidGenerator.Create(), "Djibouti", "+253"),
        new Country(guidGenerator.Create(), "Dominica", "+1-767"),
        new Country(guidGenerator.Create(), "Dominican Republic", "+1-809"),
        new Country(guidGenerator.Create(), "Ecuador", "+593"),
        new Country(guidGenerator.Create(), "Egypt", "+20"),
        new Country(guidGenerator.Create(), "El Salvador", "+503"),
        new Country(guidGenerator.Create(), "Equatorial Guinea", "+240"),
        new Country(guidGenerator.Create(), "Eritrea", "+291"),
        new Country(guidGenerator.Create(), "Estonia", "+372"),
        new Country(guidGenerator.Create(), "Eswatini (fmr. \"Swaziland\")", "+268"),
        new Country(guidGenerator.Create(), "Ethiopia", "+251"),
        new Country(guidGenerator.Create(), "Fiji", "+679"),
        new Country(guidGenerator.Create(), "Finland", "+358"),
        new Country(guidGenerator.Create(), "France", "+33"),
        new Country(guidGenerator.Create(), "Gabon", "+241"),
        new Country(guidGenerator.Create(), "Gambia", "+220"),
        new Country(guidGenerator.Create(), "Georgia", "+995"),
        new Country(guidGenerator.Create(), "Germany", "+49"),
        new Country(guidGenerator.Create(), "Ghana", "+233"),
        new Country(guidGenerator.Create(), "Greece", "+30"),
        new Country(guidGenerator.Create(), "Grenada", "+1-473"),
        new Country(guidGenerator.Create(), "Guatemala", "+502"),
        new Country(guidGenerator.Create(), "Guinea", "+224"),
        new Country(guidGenerator.Create(), "Guinea-Bissau", "+245"),
        new Country(guidGenerator.Create(), "Guyana", "+592"),
        new Country(guidGenerator.Create(), "Haiti", "+509"),
        new Country(guidGenerator.Create(), "Holy See", "+379"),
        new Country(guidGenerator.Create(), "Honduras", "+504"),
        new Country(guidGenerator.Create(), "Hungary", "+36"),
        new Country(guidGenerator.Create(), "Iceland", "+354"),
        new Country(guidGenerator.Create(), "India", "+91"),
        new Country(guidGenerator.Create(), "Indonesia", "+62"),
        new Country(guidGenerator.Create(), "Iran", "+98"),
        new Country(guidGenerator.Create(), "Iraq", "+964"),
        new Country(guidGenerator.Create(), "Ireland", "+353"),
        new Country(guidGenerator.Create(), "Israel", "+972"),
        new Country(guidGenerator.Create(), "Italy", "+39"),
        new Country(guidGenerator.Create(), "Jamaica", "+1-876"),
        new Country(guidGenerator.Create(), "Japan", "+81"),
        new Country(guidGenerator.Create(), "Jordan", "+962"),
        new Country(guidGenerator.Create(), "Kazakhstan", "+7"),
        new Country(guidGenerator.Create(), "Kenya", "+254"),
        new Country(guidGenerator.Create(), "Kiribati", "+686"),
        new Country(guidGenerator.Create(), "Kuwait", "+965"),
        new Country(guidGenerator.Create(), "Kyrgyzstan", "+996"),
        new Country(guidGenerator.Create(), "Laos", "+856"),
        new Country(guidGenerator.Create(), "Latvia", "+371"),
        new Country(guidGenerator.Create(), "Lebanon", "+961"),
        new Country(guidGenerator.Create(), "Lesotho", "+266"),
        new Country(guidGenerator.Create(), "Liberia", "+231"),
        new Country(guidGenerator.Create(), "Libya", "+218"),
        new Country(guidGenerator.Create(), "Liechtenstein", "+423"),
        new Country(guidGenerator.Create(), "Lithuania", "+370"),
        new Country(guidGenerator.Create(), "Luxembourg", "+352"),
        new Country(guidGenerator.Create(), "Madagascar", "+261"),
        new Country(guidGenerator.Create(), "Malawi", "+265"),
        new Country(guidGenerator.Create(), "Malaysia", "+60"),
        new Country(guidGenerator.Create(), "Maldives", "+960"),
        new Country(guidGenerator.Create(), "Mali", "+223"),
        new Country(guidGenerator.Create(), "Malta", "+356"),
        new Country(guidGenerator.Create(), "Marshall Islands", "+692"),
        new Country(guidGenerator.Create(), "Mauritania", "+222"),
        new Country(guidGenerator.Create(), "Mauritius", "+230"),
        new Country(guidGenerator.Create(), "Mexico", "+52"),
        new Country(guidGenerator.Create(), "Micronesia", "+691"),
        new Country(guidGenerator.Create(), "Moldova", "+373"),
        new Country(guidGenerator.Create(), "Monaco", "+377"),
        new Country(guidGenerator.Create(), "Mongolia", "+976"),
        new Country(guidGenerator.Create(), "Montenegro", "+382"),
        new Country(guidGenerator.Create(), "Morocco", "+212"),
        new Country(guidGenerator.Create(), "Mozambique", "+258"),
        new Country(guidGenerator.Create(), "Myanmar (formerly Burma)", "+95"),
        new Country(guidGenerator.Create(), "Namibia", "+264"),
        new Country(guidGenerator.Create(), "Nauru", "+674"),
        new Country(guidGenerator.Create(), "Nepal", "+977"),
        new Country(guidGenerator.Create(), "Netherlands", "+31"),
        new Country(guidGenerator.Create(), "New Zealand", "+64"),
        new Country(guidGenerator.Create(), "Nicaragua", "+505"),
        new Country(guidGenerator.Create(), "Niger", "+227"),
        new Country(guidGenerator.Create(), "Nigeria", "+234"),
        new Country(guidGenerator.Create(), "North Korea", "+850"),
        new Country(guidGenerator.Create(), "North Macedonia", "+389"),
        new Country(guidGenerator.Create(), "Norway", "+47"),
        new Country(guidGenerator.Create(), "Oman", "+968"),
        new Country(guidGenerator.Create(), "Pakistan", "+92"),
        new Country(guidGenerator.Create(), "Palau", "+680"),
        new Country(guidGenerator.Create(), "Palestine State", "+970"),
        new Country(guidGenerator.Create(), "Panama", "+507"),
        new Country(guidGenerator.Create(), "Papua New Guinea", "+675"),
        new Country(guidGenerator.Create(), "Paraguay", "+595"),
        new Country(guidGenerator.Create(), "Peru", "+51"),
        new Country(guidGenerator.Create(), "Philippines", "+63"),
        new Country(guidGenerator.Create(), "Poland", "+48"),
        new Country(guidGenerator.Create(), "Portugal", "+351"),
        new Country(guidGenerator.Create(), "Qatar", "+974"),
        new Country(guidGenerator.Create(), "Romania", "+40"),
        new Country(guidGenerator.Create(), "Russia", "+7"),
        new Country(guidGenerator.Create(), "Rwanda", "+250"),
        new Country(guidGenerator.Create(), "Saint Kitts and Nevis", "+1-869"),
        new Country(guidGenerator.Create(), "Saint Lucia", "+1-758"),
        new Country(guidGenerator.Create(), "Saint Vincent and the Grenadines", "+1-784"),
        new Country(guidGenerator.Create(), "Samoa", "+685"),
        new Country(guidGenerator.Create(), "San Marino", "+378"),
        new Country(guidGenerator.Create(), "Sao Tome and Principe", "+239"),
        new Country(guidGenerator.Create(), "Saudi Arabia", "+966"),
        new Country(guidGenerator.Create(), "Senegal", "+221"),
        new Country(guidGenerator.Create(), "Serbia", "+381"),
        new Country(guidGenerator.Create(), "Seychelles", "+248"),
        new Country(guidGenerator.Create(), "Sierra Leone", "+232"),
        new Country(guidGenerator.Create(), "Singapore", "+65"),
        new Country(guidGenerator.Create(), "Slovakia", "+421"),
        new Country(guidGenerator.Create(), "Slovenia", "+386"),
        new Country(guidGenerator.Create(), "Solomon Islands", "+677"),
        new Country(guidGenerator.Create(), "Somalia", "+252"),
        new Country(guidGenerator.Create(), "South Africa", "+27"),
        new Country(guidGenerator.Create(), "South Korea", "+82"),
        new Country(guidGenerator.Create(), "South Sudan", "+211"),
        new Country(guidGenerator.Create(), "Spain", "+34"),
        new Country(guidGenerator.Create(), "Sri Lanka", "+94"),
        new Country(guidGenerator.Create(), "Sudan", "+249"),
        new Country(guidGenerator.Create(), "Suriname", "+597"),
        new Country(guidGenerator.Create(), "Sweden", "+46"),
        new Country(guidGenerator.Create(), "Switzerland", "+41"),
        new Country(guidGenerator.Create(), "Syria", "+963"),
        new Country(guidGenerator.Create(), "Tajikistan", "+992"),
        new Country(guidGenerator.Create(), "Tanzania", "+255"),
        new Country(guidGenerator.Create(), "Thailand", "+66"),
        new Country(guidGenerator.Create(), "Timor-Leste", "+670"),
        new Country(guidGenerator.Create(), "Togo", "+228"),
        new Country(guidGenerator.Create(), "Tonga", "+676"),
        new Country(guidGenerator.Create(), "Trinidad and Tobago", "+1-868"),
        new Country(guidGenerator.Create(), "Tunisia", "+216"),
        new Country(guidGenerator.Create(), "Turkey", "+90"),
        new Country(guidGenerator.Create(), "Turkmenistan", "+993"),
        new Country(guidGenerator.Create(), "Tuvalu", "+688"),
        new Country(guidGenerator.Create(), "Uganda", "+256"),
        new Country(guidGenerator.Create(), "Ukraine", "+380"),
        new Country(guidGenerator.Create(), "United Arab Emirates", "+971"),
        new Country(guidGenerator.Create(), "United Kingdom", "+44"),
        new Country(guidGenerator.Create(), "United States", "+1"),
        new Country(guidGenerator.Create(), "Uruguay", "+598"),
        new Country(guidGenerator.Create(), "Uzbekistan", "+998"),
        new Country(guidGenerator.Create(), "Vanuatu", "+678"),
        new Country(guidGenerator.Create(), "Venezuela", "+58"),
        new Country(guidGenerator.Create(), "Vietnam", "+84"),
        new Country(guidGenerator.Create(), "Yemen", "+967"),
        new Country(guidGenerator.Create(), "Zambia", "+260"),
        new Country(guidGenerator.Create(), "Zimbabwe", "+263"),

    };

            await countryRepository.InsertManyAsync(countries, true);

            return countries.Select(co => co.Id).ToList();

        }
        #endregion

        #region Cities
        private async Task<List<Guid>> SeedCitiesAsync(List<Guid> countryKeys)
        {
            if (await cityRepository.GetCountAsync() > 0)
                return new List<Guid>();

            var cities = new List<City>
    {
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Adana"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Adıyaman"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Afyonkarahisar"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Ağrı"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Amasya"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Ankara"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Antalya"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Artvin"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Aydın"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Balıkesir"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Bilecik"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Bingöl"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Bitlis"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Bolu"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Burdur"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Bursa"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Çanakkale"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Çankırı"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Çorum"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Denizli"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Diyarbakır"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Edirne"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Elâzığ"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Erzincan"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Erzurum"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Eskişehir"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Gaziantep"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Giresun"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Gümüşhane"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Hakkâri"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Hatay"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Isparta"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Mersin"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "İstanbul"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "İzmir"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Kars"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Kastamonu"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Kayseri"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Kırklareli"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Kırşehir"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Kocaeli"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Konya"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Kütahya"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Malatya"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Manisa"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Kahramanmaraş"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Mardin"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Muğla"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Muş"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Nevşehir"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Niğde"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Ordu"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Rize"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Sakarya"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Samsun"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Siirt"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Sinop"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Sivas"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Tekirdağ"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Tokat"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Trabzon"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Tunceli"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Şanlıurfa"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Uşak"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Van"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Yozgat"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Zonguldak"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Aksaray"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Bayburt"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Karaman"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Kırıkkale"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Batman"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Şırnak"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Bartın"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Ardahan"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Iğdır"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Yalova"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Karabük"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Kilis"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Osmaniye"),
        new City(guidGenerator.Create(), countryKeys.First(c => c == countryKeys.ElementAt(178)), "Düzce")

    };

            await cityRepository.InsertManyAsync(cities, true);

            return cities.Select(c => c.Id).ToList();

        }
        #endregion

        #region Districts
        private async Task<List<Guid>> SeedDistrictsAsync(List<Guid> cityKeys)
        {
            if (await districtRepository.GetCountAsync() > 0)
                return new List<Guid>();

            var districts = new List<District>
    {
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Adalar"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Arnavutköy"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Ataşehir"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Avcılar"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Bağcılar"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Bahçelievler"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Bakırköy"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Başakşehir"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Bayrampaşa"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Beşiktaş"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Beykoz"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Beylikdüzü"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Beyoğlu"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Büyükçekmece"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Çatalca"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Çekmeköy"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Esenler"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Esenyurt"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Eyüpsultan"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Fatih"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Gaziosmanpaşa"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Güngören"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Kadıköy"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Kağıthane"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Kartal"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Küçükçekmece"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Maltepe"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Pendik"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Sancaktepe"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Sarıyer"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Şile"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Silivri"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Şişli"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Sultanbeyli"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Sultangazi"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Tuzla"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Ümraniye"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Üsküdar"),
        new District(guidGenerator.Create(), cityKeys.First(c => c == cityKeys.ElementAt(33)), "Zeytinburnu")



    };

            await districtRepository.InsertManyAsync(districts, true);

            return districts.Select(d => d.Id).ToList();

        }
        #endregion

        #region Villages
        private async Task SeedVillagesAsync(List<Guid> districtKeys)
        {
            if (await villageRepository.GetCountAsync() > 0)
                return;

            var villages = new List<Village>
    {
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(0)), "Burgazada"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(0)), "Heybeliada"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(0)), "Kınalıada"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(0)), "Büyükada"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(0)), "Maden"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(0)), "Nizam"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(0)), "Sedefadası"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(0)), "Yassıada"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(0)), "Kaşıkadası"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(0)), "Tavşanadası"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(1)), "Boğazköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(1)), "Haraççı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(1)), "Durusu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(1)), "Taşoluk"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(1)), "İmrahor"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(1)), "Hadımköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(1)), "Yassıören"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(1)), "Hacımaşlı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(1)), "Bahşayış"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(1)), "Yeniköy"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(2)), "İçerenköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(2)), "Kayışdağı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(2)), "Barbaros"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(2)), "Örnek"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(2)), "Atatürk"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(2)), "Esatpaşa"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(2)), "Fetih"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(2)), "Küçükbakkalköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(2)), "Mevlana"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(2)), "Yeni Sahra"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(3)), "Ambarlı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(3)), "Denizköşkler"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(3)), "Gümüşpala"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(3)), "Mustafa Kemal Paşa"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(3)), "Merkez"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(3)), "Firuzköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(3)), "Cihangir"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(3)), "Tahtakale"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(3)), "Yeşilkent"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(3)), "Esenkent"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(3)), "Ambarlı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(3)), "Denizköşkler"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(3)), "Gümüşpala"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(3)), "Mustafa Kemal Paşa"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(3)), "Merkez"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(3)), "Firuzköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(3)), "Cihangir"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(3)), "Tahtakale"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(3)), "Üniversite"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(3)), "Bağlar"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(4)), "Barbaros"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(4)), "Çınar"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(4)), "100. Yıl"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(4)), "Demirkapı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(4)), "15 Temmuz"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(4)), "Fatih"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(4)), "Kirazlı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(4)), "Göztepe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(4)), "Merkez"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(4)), "Hürriyet"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(5)), "İnönü"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(5)), "Kazım Karabekir"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(5)), "Kemalpaşa"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(5)), "Fevzi Çakmak"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(5)), "Güneşli"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(5)), "Sancaktepe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(5)), "Yavuz Selim"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(5)), "Yenigün"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(5)), "Yenimahalle"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(5)), "Yıldıztepe"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(6)), "Mahmutbey"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(6)), "Siyavuşpaşa"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(6)), "Soğanlı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(6)), "Şirinevler"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(6)), "Yenibosna Merkez"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(6)), "Zafer"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(6)), "Bahçelievler"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(6)), "Cumhuriyet"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(6)), "Çobançeşme"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(6)), "Fevzi Çakmak"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(6)), "Hürriyet"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(6)), "Kocasinan Merkez"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(6)), "Ataköy 1. Kısım"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(6)), "Ataköy 2. 5. 6. Kısım"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(6)), "Ataköy 3-4-11. Kısım"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(6)), "Ataköy 7-8-9-10. Kısım"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(6)), "Basınköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(6)), "Cevizlik"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(6)), "Kartaltepe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(6)), "Osmaniye"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(7)), "Sakızağacı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(7)), "Şenlikköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(7)), "Yenimahalle"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(7)), "Yeşilköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(7)), "Zeytinlik"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(7)), "Zuhuratbaba"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(7)), "Yeşilyurt"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(7)), "Bahçeşehir 2. Kısım"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(7)), "Başak"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(7)), "Kayabaşı"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(8)), "Altınşehir"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(8)), "Ziya Gökalp"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(8)), "Bahçeşehir 1. Kısım"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(8)), "Başakşehir"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(8)), "Güvercintepe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(8)), "Şahintepesi"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(8)), "Şamlar"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(8)), "Altıntepsi"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(8)), "Muratpaşa"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(8)), "Orta"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(9)), "Terazidere"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(9)), "Vatan"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(9)), "Yenidoğan"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(9)), "Yıldırım"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(9)), "Cevatpaşa"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(9)), "İsmetpaşa"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(9)), "Kartaltepe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(9)), "Kocatepe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(9)), "Gayrettepe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(9)), "Konaklar"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(9)), "Kuruçeşme"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(9)), "Kültür"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(9)), "Levazım"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(9)), "Levent"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(9)), "Mecidiye"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(9)), "Muradiye"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(9)), "Nisbetiye"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(9)), "Ortaköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(9)), "Abbasağa"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(9)), "Akat"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(10)), "Arnavutköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(10)), "Balmumcu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(10)), "Bebek"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(10)), "Cihannüma"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(10)), "Dikilitaş"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(10)), "Etiler"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(10)), "Sinanpaşa"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(10)), "Türkali"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(10)), "Vişnezade"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(10)), "Yıldız"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(11)), "Ulus"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(11)), "Çengeldere"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(11)), "Yavuz Selim"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(11)), "Fatih"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(11)), "Baklacı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(11)), "Çiftlik"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(11)), "Riva"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(11)), "Acarlar"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(11)), "İshaklı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(11)), "Cumhuriyet"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(12)), "Göllü"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(12)), "Paşamandıra"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(12)), "Zerzavatçı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(12)), "Bozhane"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(12)), "Mahmutşevketpaşa"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(12)), "Kılıçlı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(12)), "Öğümce"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(12)), "Tokatköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(12)), "Yalıköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(12)), "Yeni Mahalle"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(13)), "Soğuksu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(13)), "Anadolu Hisarı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(13)), "Anadolu Kavağı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(13)), "Beykoz Merkez"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(13)), "Çamlıbahçe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(13)), "Çiğdem"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(13)), "Çubuklu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(13)), "Göksu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(13)), "Göztepe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(13)), "Gümüşsuyu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(13)), "İncirköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(13)), "Kanlıca"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(13)), "Kavacık"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(13)), "Ortaçeşme"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(13)), "Paşabahçe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(13)), "Rüzgarlıbahçe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(13)), "Örnekköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(13)), "Akbaba"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(13)), "Alibahadır"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(13)), "Anadolufeneri"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(14)), "Dereseki"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(14)), "Elmalı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(14)), "Görele"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(14)), "Kaynarca"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(14)), "Polonezköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(14)), "Poyrazköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(14)), "Cumhuriyet"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(14)), "Yakup"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(14)), "Adnan Kahveci"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(14)), "Marmara"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(15)), "Dereagzi"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(15)), "Kavakli"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(15)), "Büyükşehir"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(15)), "Sahil"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(15)), "Barış"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(15)), "Gürpınar"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(15)), "Acarlar"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(15)), "İshakli"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(15)), "Cumhuriyet"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(15)), "Paşamandira"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(16)), "Bozhan"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(16)), "Mahmutşevketpaşa"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(16)), "Kılıçlı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(16)), "Öğümce"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(16)), "Tokatköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(16)), "Yalıköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(16)), "Yeni Mahalle"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(16)), "Soğuksu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(16)), "Anadolu Hisarı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(16)), "Anadolu Kavağı"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(17)), "Beykoz Merkez"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(17)), "Çamlıbahçe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(17)), "Çiğdem"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(17)), "Çubuklu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(17)), "Göksu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(17)), "Göztepe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(17)), "Gümüşsuyu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(17)), "İncirköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(17)), "Kanlıca"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(17)), "Kavacık"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(17)), "Ortaçeşme"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(17)), "Paşabahçe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(17)), "Rüzgarlıbahçe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(17)), "Örnekköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(17)), "Akbaba"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(17)), "Alibahadır"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(17)), "Anadolufeneri"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(17)), "Dereseki"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(17)), "Elmalı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(17)), "Görele"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(18)), "Kaynarca"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(18)), "Polonezköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(18)), "Poyrazköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(18)), "Cumhuriyet"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(18)), "Yakup"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(18)), "Adnan Kahveci"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(18)), "Marmara"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(18)), "Dereagzi"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(18)), "Kavakli"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(18)), "Büyükşehir"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(19)), "Sahil"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(19)), "Barış"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(19)), "Gürpınar"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(19)), "Acarlar"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(19)), "İshaklı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(19)), "Cumhuriyet"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(19)), "Paşamandıra"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(19)), "Bozhane"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(19)), "Mahmutşevketpaşa"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(19)), "Kılıçlı"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(20)), "Öğümce"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(20)), "Tokatköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(20)), "Yalıköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(20)), "Yeni Mahalle"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(20)), "Soğuksu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(20)), "Anadolu Hisarı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(20)), "Anadolu Kavağı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(20)), "Beykoz Merkez"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(20)), "Çamlıbahçe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(20)), "Çiğdem"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(21)), "Çubuklu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(21)), "Göksu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(21)), "Göztepe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(21)), "Gümüşsuyu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(21)), "İncirköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(21)), "Kanlıca"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(21)), "Kavacık"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(21)), "Ortaçeşme"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(21)), "Paşabahçe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(21)), "Rüzgarlıbahçe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(21)), "Örnekköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(21)), "Akbaba"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(21)), "Alibahadır"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(21)), "Anadolufeneri"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(21)), "Dereseki"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(21)), "Elmalı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(21)), "Görele"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(21)), "Kaynarca"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(21)), "Polonezköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(21)), "Poyrazköy"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(22)), "Cumhuriyet"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(22)), "Yakup"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(22)), "Adnan Kahveci"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(22)), "Marmara"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(22)), "Dereagzi"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(22)), "Kavakli"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(22)), "Büyükşehir"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(22)), "Sahil"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(22)), "Barış"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(22)), "Gürpınar"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(23)), "Acarlar"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(23)), "İshaklı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(23)), "Cumhuriyet"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(23)), "Paşamandıra"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(23)), "Bozhane"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(23)), "Mahmutşevketpaşa"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(23)), "Kılıçlı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(23)), "Öğümce"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(23)), "Tokatköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(23)), "Yalıköy"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(24)), "Yeni Mahalle"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(24)), "Soğuksu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(24)), "Anadolu Hisarı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(24)), "Anadolu Kavağı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(24)), "Beykoz Merkez"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(24)), "Çamlıbahçe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(24)), "Çiğdem"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(24)), "Çubuklu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(24)), "Göksu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(24)), "Göztepe"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(25)), "Gümüşsuyu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(25)), "İncirköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(25)), "Kanlıca"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(25)), "Kavacık"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(25)), "Ortaçeşme"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(25)), "Paşabahçe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(25)), "Rüzgarlıbahçe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(25)), "Örnekköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(25)), "Akbaba"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(25)), "Alibahadır"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(25)), "Anadolufeneri"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(25)), "Dereseki"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(25)), "Elmalı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(25)), "Görele"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(25)), "Kaynarca"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(25)), "Polonezköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(25)), "Poyrazköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(25)), "Cumhuriyet"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(25)), "Yakup"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(25)), "Adnan Kahveci"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(26)), "Marmara"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(26)), "Dereagzi"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(26)), "Kavakli"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(26)), "Büyükşehir"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(26)), "Sahil"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(26)), "Barış"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(26)), "Gürpınar"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(26)), "Acarlar"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(26)), "İshaklı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(26)), "Cumhuriyet"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(27)), "Paşamandıra"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(27)), "Bozhane"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(27)), "Mahmutşevketpaşa"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(27)), "Kılıçlı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(27)), "Öğümce"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(27)), "Tokatköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(27)), "Yalıköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(27)), "Yeni Mahalle"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(27)), "Soğuksu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(27)), "Anadolu Hisarı"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(28)), "Anadolu Kavağı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(28)), "Beykoz Merkez"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(28)), "Çamlıbahçe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(28)), "Çiğdem"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(28)), "Çubuklu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(28)), "Göksu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(28)), "Göztepe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(28)), "Gümüşsuyu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(28)), "İncirköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(28)), "Kanlıca"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(29)), "Kavacık"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(29)), "Ortaçeşme"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(29)), "Paşabahçe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(29)), "Rüzgarlıbahçe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(29)), "Örnekköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(29)), "Akbaba"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(29)), "Alibahadır"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(29)), "Anadolufeneri"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(29)), "Dereseki"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(29)), "Elmalı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(29)), "Görele"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(29)), "Kaynarca"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(29)), "Polonezköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(29)), "Poyrazköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(29)), "Cumhuriyet"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(29)), "Yakup"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(29)), "Adnan Kahveci"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(29)), "Marmara"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(29)), "Dereagzi"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(29)), "Kavakli"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(30)), "Büyükşehir"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(30)), "Sahil"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(30)), "Barış"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(30)), "Gürpınar"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(30)), "Acarlar"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(30)), "İshaklı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(30)), "Cumhuriyet"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(30)), "Paşamandıra"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(30)), "Bozhane"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(30)), "Mahmutşevketpaşa"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(31)), "Kılıçlı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(31)), "Öğümce"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(31)), "Tokatköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(31)), "Yalıköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(31)), "Yeni Mahalle"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(31)), "Soğuksu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(31)), "Anadolu Hisarı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(31)), "Anadolu Kavağı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(31)), "Beykoz Merkez"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(31)), "Çamlıbahçe"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(32)), "Çiğdem"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(32)), "Çubuklu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(32)), "Göksu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(32)), "Göztepe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(32)), "Gümüşsuyu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(32)), "İncirköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(32)), "Kanlıca"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(32)), "Kavacık"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(32)), "Ortaçeşme"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(32)), "Paşabahçe"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(33)), "Rüzgarlıbahçe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(33)), "Örnekköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(33)), "Akbaba"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(33)), "Alibahadır"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(33)), "Anadolufeneri"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(33)), "Dereseki"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(33)), "Elmalı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(33)), "Görele"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(33)), "Kaynarca"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(33)), "Polonezköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(33)), "Poyrazköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(33)), "Cumhuriyet"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(33)), "Yakup"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(33)), "Adnan Kahveci"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(33)), "Marmara"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(33)), "Dereagzi"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(33)), "Kavakli"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(33)), "Büyükşehir"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(33)), "Sahil"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(33)), "Barış"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(34)), "Gürpınar"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(34)), "Acarlar"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(34)), "İshaklı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(34)), "Cumhuriyet"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(34)), "Paşamandıra"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(34)), "Bozhane"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(34)), "Mahmutşevketpaşa"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(34)), "Kılıçlı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(34)), "Öğümce"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(34)), "Tokatköy"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(35)), "Yalıköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(35)), "Yeni Mahalle"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(35)), "Soğuksu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(35)), "Anadolu Hisarı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(35)), "Anadolu Kavağı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(35)), "Beykoz Merkez"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(35)), "Çamlıbahçe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(35)), "Çiğdem"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(35)), "Çubuklu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(35)), "Göksu"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(36)), "Göztepe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(36)), "Gümüşsuyu"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(36)), "İncirköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(36)), "Kanlıca"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(36)), "Ortaçeşme"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(36)), "Paşabahçe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(36)), "Rüzgarlıbahçe"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(36)), "Örnekköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(36)), "Akbaba"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(36)), "Alibahadır"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(36)), "Anadolufeneri"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(36)), "Dereseki"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(36)), "Elmalı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(36)), "Görele"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(37)), "Kaynarca"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(37)), "Polonezköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(37)), "Poyrazköy"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(37)), "Cumhuriyet"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(37)), "Yakup"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(37)), "Adnan Kahveci"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(37)), "Marmara"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(37)), "Dereagzi"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(37)), "Kavakli"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(37)), "Büyükşehir"),

        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(38)), "Sahil"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(38)), "Barış"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(38)), "Gürpınar"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(38)), "Acarlar"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(38)), "İshaklı"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(38)), "Cumhuriyet"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(38)), "Paşamandıra"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(38)), "Bozhane"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(38)), "Mahmutşevketpaşa"),
        new Village(guidGenerator.Create(), districtKeys.First(d => d == districtKeys.ElementAt(38)), "Kılıçlı")

    };

            await villageRepository.InsertManyAsync(villages, true);

        }
        #endregion

        #region TestGroups

        private async Task<List<Guid>> SeedTestGroupsAsync()
        {
            if (await testGroupRepository.GetCountAsync() > 0)
                return new List<Guid>();

            var testGroups = new List<TestGroup>
            {
                new TestGroup(Guid.NewGuid(), "Hematology"),
                new TestGroup(Guid.NewGuid(), "Chemistry"),
                new TestGroup(Guid.NewGuid(), "Pathology"),
                new TestGroup(Guid.NewGuid(), "Microbiology")
            };

            await testGroupRepository.InsertManyAsync(testGroups, true);

            return testGroups.Select(d => d.Id).ToList();

        }
        #endregion

        #region TestGroupItems

        private async Task<List<Guid>> SeedTestGroupItemsAsync(List<Guid> testGroupKeys)
        {
            if (await testGroupItemRepository.GetCountAsync() > 0)
                return new List<Guid>();

            var testGroupItems = new List<TestGroupItem>
            {
               new TestGroupItem(
                guidGenerator.Create(),
                testGroupKeys.ElementAt(0),
                "Eosinophils/100 leukocytes in Sputum by Manual count",
                "10327-5",
                "Laboratory",
                "Measures the components of blood.",
                24
               ),
               new TestGroupItem(
                guidGenerator.Create(),
                testGroupKeys.ElementAt(0),
                "Lymphocytes/100 leukocytes in Cerebral spinal fluid by Manual count",
                "10328-3",
                "Laboratory",
                null,
                8
               ),
               new TestGroupItem(
                guidGenerator.Create(),
                testGroupKeys.ElementAt(0),
                "Monocytes/100 leukocytes in Cerebral spinal fluid by Manual count",
                "10329-1",
                "Laboratory",
                null,
                7
               ),
               new TestGroupItem(
                guidGenerator.Create(),
                testGroupKeys.ElementAt(0),
                "Monocytes/100 leukocytes in Body fluid by Manual count",
                "10330-9",
                "Laboratory",
                null,
                7
               ),
                new TestGroupItem(
            guidGenerator.Create(),
                testGroupKeys.ElementAt(0),
            "Hemoglobin A [Units/volume] in Blood by Electrophoresis",
            "10346-5",
            "Hematology",
            "Measures Hemoglobin A in blood by electrophoresis.",
            24
        ),
        new TestGroupItem(
            guidGenerator.Create(),
                testGroupKeys.ElementAt(0),
            "Bite cells [Presence] in Blood by Light microscopy",
            "10371-3",
            "Hematology",
            "Detects bite cells in blood using light microscopy.",
            36
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(0),
            "Blister cells [Presence] in Blood by Light microscopy",
            "10372-1",
            "Hematology",
            "Detects blister cells in blood using light microscopy.",
            48
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(0),
            "Fragments [Presence] in Blood by Light microscopy",
            "10373-9",
            "Hematology",
            null,
            30
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(0),
            "Helmet cells [Presence] in Blood by Light microscopy",
            "10374-7",
            "Hematology",
            null,
            36
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(0),
            "Oval macrocytes [Presence] in Blood by Light microscopy",
            "10376-2",
            "Hematology",
            "Detects oval macrocytes in blood using light microscopy.",
            24
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(0),
            "Pencil cells [Presence] in Blood by Light microscopy",
            "10377-0",
            "Hematology",
            "Detects pencil cells in blood using light microscopy.",
            36
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(0),
            "Polychromasia [Presence] in Blood by Light microscopy",
            "10378-8",
            "Hematology",
            "Detects polychromasia in blood using light microscopy.",
            48
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(0),
            "Erythrocytes.dual population [Presence] in Blood by Light microscopy",
            "10379-6",
            "Hematology",
            "Detects dual population erythrocytes in blood using light microscopy.",
            30
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(0),
            "Stomatocytes [Presence] in Blood by Light microscopy",
            "10380-4",
            "Hematology",
            "Detects stomatocytes in blood using light microscopy.",
            36
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(0),
            "Target cells [Presence] in Blood by Light microscopy",
            "10381-2",
            "Hematology",
            "Detects target cells in blood using light microscopy.",
            42
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(0),
            "Complement C1 esterase inhibitor.total in Serum or Plasma",
            "10634-4",
            "Hematology",
            "Measures functional and total levels of Complement C1 esterase inhibitor in serum or plasma.",
            72
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(1),
            "Oxygen content in Intravascular space",
            "10226-9",
            "Hematology",
            "Determines the oxygen content in intravascular space.",
            24
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(1),
            "Oxygen content in Aortic root",
            "10232-7",
            "Hematology",
            "Determines the oxygen content in the aortic root.",
            36
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(1),
            "Oxygen content in Left atrium",
            "10233-5",
            "Hematology",
            "Determines the oxygen content in the left atrium.",
            48
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(1),
            "Oxygen content in Right atrium",
            "10234-3",
            "Hematology",
            "Determines the oxygen content in the right atrium.",
            30
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(1),
            "Oxygen content in High right atrium",
            "10235-0",
            "Hematology",
            "Determines the oxygen content in the high right atrium.",
            42
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(1),
            "Oxygen content in Low right atrium",
            "10236-8",
            "Hematology",
            "Determines the oxygen content in the low right atrium.",
            54
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(1),
            "Oxygen content in Mid right atrium",
            "10237-6",
            "Hematology",
            "Measures oxygen content in the mid-right atrium of the heart.",
            2
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(1),
            "Oxygen content in Left ventricle",
            "10238-4",
            "Hematology",
            "Measures oxygen content in the left ventricle of the heart.",
            2
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(1),
            "Oxygen content in Right ventricular outflow tract",
            "10239-2",
            "Hematology",
            "Measures oxygen content in the right ventricular outflow tract.",
            3
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(1),
            "Oxygen content in Right ventricle",
            "10240-0",
            "Hematology",
            "Measures oxygen content in the right ventricle of the heart.",
            3
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(1),
            "Oxygen content in Coronary sinus",
            "10241-8",
            "Hematology",
            "Measures oxygen content in the coronary sinus.",
            4
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(1),
            "Oxygen content in Ductus",
            "10242-6",
            "Hematology",
            "Measures oxygen content in the ductus arteriosus or ductus venosus.",
            4
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(2),
            "Actin Ag Test",
            "10457-0",
            "Immunohistochemistry",
            "Test for presence of Actin antigen in tissue.",
            24
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(2),
            "ALP Placenta Ag Test",
            "10458-8",
            "Immunohistochemistry",
            "Test for placental alkaline phosphatase antigen in tissue.",
            48
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(2),
            "AFP Ag Test",
            "10459-6",
            "Immunohistochemistry",
            "Test for Alpha-1-Fetoprotein antigen in tissue.",
            12
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(2),
            "Alpha Lactalbumin Ag Test",
            "10460-4",
            "Immunohistochemistry",
            "Test for Lactalbumin alpha antigen in tissue.",
            36
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(2),
            "A1ACT Ag Test",
            "10461-2",
            "Immunohistochemistry",
            "Test for Alpha-1-Antichymotrypsin antigen in tissue.",
            48
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(2),
            "A1AT Ag Test",
            "10462-0",
            "Immunohistochemistry",
            "Test for Alpha 1 antitrypsin antigen in tissue.",
            24
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(2),
            "ALA Ag Test",
            "10463-8",
            "Immunohistochemistry",
            "Test for Amyloid A component antigen in tissue.",
            18
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(2),
            "ALP Ag Test",
            "10464-6",
            "Immunohistochemistry",
            "Test for Amyloid P component antigen in tissue.",
            36
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(2),
            "AL Prealbumin Ag Test",
            "10465-3",
            "Immunohistochemistry",
            "Test for Amyloid prealbumin antigen in tissue.",
            24
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(2),
            "Beta-2-Microglobulin Ag Test",
            "10467-9",
            "Immunohistochemistry",
            "Test for Beta-2-Microglobulin antigen in tissue.",
            30
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(2),
            "Calcitonin Ag Test",
            "10468-7",
            "Immunohistochemistry",
            "Test for Calcitonin antigen in tissue.",
            18
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(2),
            "Carcinoembryonic Ag Test",
            "10469-5",
            "Immunohistochemistry",
            "Test for Carcinoembryonic antigen in tissue.",
            24
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(2),
            "Jones MS Stn Test",
            "10792-0",
            "Histology",
            "Microscopic observation in tissue by Methenamine silver stain Jones.",
            20
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(2),
            "MG Stn Test",
            "10793-8",
            "Histology",
            "Microscopic observation in tissue by Methyl green stain.",
            12
        ),
        new TestGroupItem(
            guidGenerator.Create(),
            testGroupKeys.ElementAt(2),
            "MG-Pyronine Y Stn Test",
            "10794-6",
            "Histology",
            "Microscopic observation in tissue by Methyl green-pyronine Y stain.",
            18
        ),
        new TestGroupItem(
        id: guidGenerator.Create(),
        testGroupId: testGroupKeys.ElementAt(3),
        name: "Bab microti Bld Smear",
        code: "10347-3",
        testType: "Microscopy",
        description: "Babesia microti identified in Blood by Light microscopy",
        turnaroundTime: 12
    ),
    new TestGroupItem(
        id: guidGenerator.Create(),
        testGroupId: testGroupKeys.ElementAt(3),
        name: "B parapert Ab Ser Ql",
        code: "10348-1",
        testType: "Serology",
        description: "Bordetella parapertussis Ab [Presence] in Serum",
        turnaroundTime: 24
    ),
    new TestGroupItem(
        id: guidGenerator.Create(),
        testGroupId: testGroupKeys.ElementAt(3),
        name: "Brucella Ab Ser-aCnc",
        code: "10349-9",
        testType: "Serology",
        description: "Brucella sp Ab [Units/volume] in Serum",
        turnaroundTime: 48
    ),
    new TestGroupItem(
        id: guidGenerator.Create(),
        testGroupId: testGroupKeys.ElementAt(3),
        name: "HSV IgM Titr Ser EIA",
        code: "10350-7",
        testType: "Immunoassay",
        description: "Herpes simplex virus IgM Ab [Titer] in Serum by Immunoassay",
        turnaroundTime: 36
    ),
    new TestGroupItem(
        id: guidGenerator.Create(),
        testGroupId: testGroupKeys.ElementAt(3),
        name: "HCV RNA SerPl Amp Prb-aCnc",
        code: "10676-5",
        testType: "Molecular Biology",
        description: "Hepatitis C virus RNA [Units/volume] (viral load) in Serum or Plasma by Probe with amplification",
        turnaroundTime: 72
    ),
    new TestGroupItem(
        id: guidGenerator.Create(),
        testGroupId: testGroupKeys.ElementAt(3),
        name: "HSV1 Ag Tiss Ql ImStn",
        code: "10677-3",
        testType: "Immunostaining",
        description: "Herpes simplex virus 1 Ag [Presence] in Tissue by Immune stain",
        turnaroundTime: 48
    ),
    new TestGroupItem(
        id: guidGenerator.Create(),
        testGroupId: testGroupKeys.ElementAt(3),
        name: "HSV1+2 Ag Tiss Ql ImStn",
        code: "10678-1",
        testType: "Immunostaining",
        description: "Herpes simplex virus 1+2 Ag [Presence] in Tissue by Immune stain",
        turnaroundTime: 60
    ),
    new TestGroupItem(
        id: guidGenerator.Create(),
        testGroupId: testGroupKeys.ElementAt(3),
        name: "Trypanos Bld Thick Film",
        code: "10731-8",
        testType: "Microscopy",
        description: "Trypanosoma sp identified in Blood by Thick film",
        turnaroundTime: 24
    ),
    new TestGroupItem(
        id: guidGenerator.Create(),
        testGroupId: testGroupKeys.ElementAt(3),
        name: "Trypanos Bld Thin Film",
        code: "10732-6",
        testType: "Microscopy",
        description: "Trypanosoma sp identified in Blood by Thin film",
        turnaroundTime: 18
    ),
    new TestGroupItem(
        id: guidGenerator.Create(),
        testGroupId: testGroupKeys.ElementAt(3),
        name: "Trypanos Bld Wet Prep",
        code: "10733-4",
        testType: "Microscopy",
        description: "Trypanosoma sp identified in Blood by Wet preparation",
        turnaroundTime: 12
    ),
    new TestGroupItem(
        id: guidGenerator.Create(),
        testGroupId: testGroupKeys.ElementAt(3),
        name: "VZV Skin EM",
        code: "10734-2",
        testType: "Electron Microscopy",
        description: "Varicella zoster virus identified in Skin by Electron microscopy",
        turnaroundTime: 96
    ),
    new TestGroupItem(
        id: guidGenerator.Create(),
        testGroupId: testGroupKeys.ElementAt(3),
        name: "Viral Seq Ser Seq",
        code: "10735-9",
        testType: "Sequencing",
        description: "Viral sequencing [Identifier] in Serum by Sequencing",
        turnaroundTime: 120
    ),
    new TestGroupItem(
        id: guidGenerator.Create(),
        testGroupId: testGroupKeys.ElementAt(3),
        name: "Virus CSF EM",
        code: "10736-7",
        testType: "Electron Microscopy",
        description: "Virus identified in Cerebral spinal fluid by Electron microscopy",
                turnaroundTime: 96
    )
            };

            await testGroupItemRepository.InsertManyAsync(testGroupItems, true);

            return testGroupItems.Select(c => c.Id).ToList();
        }

        #endregion

        #region Departments
        private async Task<List<Guid>> SeedDepartmentsAsync()
        {
            if (await departmentRepository.GetCountAsync() > 0)
                return new List<Guid>();
            var departments = new List<Department>
            {
                new Department(guidGenerator.Create(), "Cardiology"),
                new Department(guidGenerator.Create(), "Neurology"),
                new Department(guidGenerator.Create(), "General Surgery"),
                new Department(guidGenerator.Create(), "Dermatology"),
                new Department(guidGenerator.Create(), "Psychiatry"),
                new Department(guidGenerator.Create(), "Pediatrics")
            };
            await departmentRepository.InsertManyAsync(departments, true);
            return departments.Select(d => d.Id).ToList();

        }
        #endregion

        #region Titles
        private async Task<List<Guid>> SeedTitlesAsync()
        {
            if (await titleRepository.GetCountAsync() > 0)
                return new List<Guid>();
            var titles = new List<Title>
            {
                new Title(guidGenerator.Create(), "Prof. "),
                new Title(guidGenerator.Create(), "Doç. Dr."),
                new Title(guidGenerator.Create(), "Uz. Dr."),
                new Title(guidGenerator.Create(), "Op. Dr."),
                new Title(guidGenerator.Create(), "Asist. Dr.")
            };
            await titleRepository.InsertManyAsync(titles, true);
            return titles.Select(t => t.Id).ToList();
        }
        #endregion

        #region AppointmentTypes
        
        #endregion

        #region Companies
        private async Task<List<Guid>> SeedPatientCompaniesAsync()
        {
            if (await patientCompanyRepository.GetCountAsync() > 0)
                return new List<Guid>();
            var companies = new List<PatientCompany>
            {
                new PatientCompany(guidGenerator.Create(), "Pusula"),
                new PatientCompany(guidGenerator.Create(), "Kendine İyi Bak"),
                new PatientCompany(guidGenerator.Create(), "Sağlık Hizmetleri A.Ş."),
            };
            await patientCompanyRepository.InsertManyAsync(companies, true);
            return companies.Select(c => c.Id).ToList();
        }

        #endregion

    }
}