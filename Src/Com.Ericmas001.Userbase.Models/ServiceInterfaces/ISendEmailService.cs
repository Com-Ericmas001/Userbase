namespace Com.Ericmas001.Userbase.Models.ServiceInterfaces
{
    public interface ISendEmailService
    {
        void SendRecoveryToken(RecoveryToken token, string username, string email);
    }
}
