using System.Diagnostics.CodeAnalysis;

namespace Com.Ericmas001.Userbase.Models
{
    public interface IEmailSender
    {
        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        void SendToken(RecoveryToken token, string username, string email);
    }
}
