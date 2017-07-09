using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Com.Ericmas001.Userbase
{
    public class UserbaseConfig : IUserbaseConfig
    {
        private readonly string m_Salt;

        public UserbaseConfig(string salt)
        {
            m_Salt = salt;
        }

        public string SaltPassword(string unsaltedPassword)
        {
            return $"{m_Salt}{unsaltedPassword}";
        }
    }
}