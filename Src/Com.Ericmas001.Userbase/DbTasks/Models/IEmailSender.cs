using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Ericmas001.Userbase.Responses.Models;

namespace Com.Ericmas001.Userbase.DbTasks.Models
{
    public interface IEmailSender
    {
        void SendToken(RecoveryToken token, string email);
    }
}
