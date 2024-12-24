using Pusula.Training.HealthCare.TestProcesses;
using Pusula.Training.HealthCare.Core.EMail;
using Volo.Abp.Emailing;

namespace Pusula.Training.HealthCare.EmailServices
{
    public class TestProcessEmailService(IEmailSender emailSender) : EmailService<TestProcessEmailDto>(emailSender), ITestProcessEmailService<TestProcessEmailDto>
    {
        public override string GenerateEmailBody(TestProcessEmailDto model) => $@"<h1>Test Sonuçlarınız Çıkmıştır</h1>
            <p>Merhaba {model.PatientName},</p>
            <p>Test sonuçlarınız hazırdır. Detaylar için doktorunuzdan randevu alınız.</p>
            <p>Saygılarımızla, halleDEViz Ekibi</p>";
    }
}
