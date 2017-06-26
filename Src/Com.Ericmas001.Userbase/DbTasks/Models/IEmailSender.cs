using System.Diagnostics.CodeAnalysis;
using Com.Ericmas001.Userbase.Responses.Models;

namespace Com.Ericmas001.Userbase.DbTasks.Models
{
    public interface IEmailSender
    {
        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        void SendToken(RecoveryToken token, string username, string email);
    }
}
