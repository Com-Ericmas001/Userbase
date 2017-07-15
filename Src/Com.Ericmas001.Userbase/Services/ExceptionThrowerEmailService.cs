using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Ericmas001.Userbase.Models;
using Com.Ericmas001.Userbase.Models.ServiceInterfaces;

namespace Com.Ericmas001.Userbase.Services
{
    public class ExceptionThrowerEmailService : ISendEmailService
    {
        public void SendRecoveryToken(RecoveryToken token, string username, string email)
        {
            throw new NotImplementedException("You should Register your own ISendEmailService if you want to use recovery !!");
        }
    }
}
