using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Ericmas001.Userbase.Models;
using Com.Ericmas001.Userbase.Models.Responses;

namespace Com.Ericmas001.Userbase.Services.Interfaces
{
    public interface IUserRecoveryService
    {
        bool SendRecoveryToken(string username, IEmailSender smtp);
        ConnectUserResponse ResetPassword(string username, string recoveryToken, string newPassword);
    }
}
