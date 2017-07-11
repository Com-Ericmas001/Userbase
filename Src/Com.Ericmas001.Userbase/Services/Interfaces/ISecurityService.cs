namespace Com.Ericmas001.Userbase.Services.Interfaces
{
    public interface ISecurityService
    {
        bool VerifyPassword(string passwordToTry, string encryptedPassword);

        string EncryptPassword(string password);
    }
}
