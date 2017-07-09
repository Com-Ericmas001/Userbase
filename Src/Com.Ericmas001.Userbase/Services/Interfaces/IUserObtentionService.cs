using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Ericmas001.Userbase.Models.Responses;

namespace Com.Ericmas001.Userbase.Services.Interfaces
{
    public interface IUserObtentionService
    {
        int FromUsername(string username);
        int FromEmail(string email);

        bool UsernameExists(string username);
        bool EmailExists(string email);
    }
}
