using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Com.Ericmas001.Security.Cryptography;
using Com.Ericmas001.Userbase.Services.Interfaces;

namespace Com.Ericmas001.Userbase
{
    public class SecurityService : ISecurityService
    {
        private readonly string m_Salt;

        public SecurityService(string salt)
        {
            m_Salt = salt;
        }

        public string SaltPassword(string unsaltedPassword)
        {
            return $"{m_Salt}{unsaltedPassword}";
        }
        
        public string EncryptPassword(string password)
        {
            return BCrypt.HashPassword(SaltPassword(password), BCrypt.GenerateSalt());
        }
    }
}