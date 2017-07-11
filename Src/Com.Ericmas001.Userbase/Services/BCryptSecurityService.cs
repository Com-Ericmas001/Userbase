using Com.Ericmas001.Security.Cryptography;
using Com.Ericmas001.Userbase.Services.Interfaces;

namespace Com.Ericmas001.Userbase.Services
{
    public class BCryptSecurityService : ISecurityService
    {
        private readonly string m_Salt;

        public BCryptSecurityService(string salt)
        {
            m_Salt = salt;
        }
        
        public string EncryptPassword(string password)
        {
            return BCrypt.HashPassword(SaltPassword(password), BCrypt.GenerateSalt());
        }

        public bool VerifyPassword(string passwordToTry, string encryptedPassword)
        {
            return BCrypt.CheckPassword(SaltPassword(passwordToTry), encryptedPassword);
        }

        private string SaltPassword(string unsaltedPassword)
        {
            return $"{m_Salt}{unsaltedPassword}";
        }
    }
}