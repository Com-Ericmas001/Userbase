namespace Com.Ericmas001.Userbase.Services.Interfaces
{
    public interface ISecurityService
    {
        string SaltPassword(string unsaltedPassword);
        string EncryptPassword(string password);
    }
}
