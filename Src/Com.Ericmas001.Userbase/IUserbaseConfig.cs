using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Com.Ericmas001.Userbase
{
    public interface IUserbaseConfig
    {
        string SaltPassword(string unsaltedPassword);
    }
}
