
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Pusula.Training.HealthCare.Core.EMail
{
    public interface IEmailService : ITransientDependency
    {
        Task SendAsync(string to, string subject, string body);
        Task SendAsync(string from,string to, string subject, string body);
        Task QueueAsync(string to, string subject, string body);
        Task QueueAsync(string from,string to, string subject, string body);

        string CreateEmailBody(params object[] args);

        //IAppoinrmentemail service olucak create e mail body kaldıralacak
    }
}
