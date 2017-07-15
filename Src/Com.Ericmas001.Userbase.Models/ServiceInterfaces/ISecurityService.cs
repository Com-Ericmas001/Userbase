namespace Com.Ericmas001.Userbase.Models.ServiceInterfaces
{
    public interface ISecurityService
    {
        bool VerifyPassword(string passwordToTry, string encryptedPassword);

        string EncryptPassword(string password);
    }
}
