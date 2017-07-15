using Com.Ericmas001.Userbase.Models.Responses;

namespace Com.Ericmas001.Userbase.Models.ServiceInterfaces
{
    public interface IUserRecoveryService
    {
        bool SendRecoveryToken(string username);
        ConnectUserResponse ResetPassword(string username, string recoveryToken, string newPassword);
    }
}
