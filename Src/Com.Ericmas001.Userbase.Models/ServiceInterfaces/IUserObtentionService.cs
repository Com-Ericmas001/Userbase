namespace Com.Ericmas001.Userbase.Models.ServiceInterfaces
{
    public interface IUserObtentionService
    {
        int FromUsername(string username);
        int FromEmail(string email);

        bool UsernameExists(string username);
        bool EmailExists(string email);
    }
}
