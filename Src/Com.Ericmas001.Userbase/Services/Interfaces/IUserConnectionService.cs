using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Ericmas001.Userbase.Models;
using Com.Ericmas001.Userbase.Models.Responses;

namespace Com.Ericmas001.Userbase.Services.Interfaces
{
    public interface IUserConnectionService
    {
        ConnectUserResponse ConnectWithToken(string username, Guid token);
        ConnectUserResponse ConnectWithPassword(string username, string password);
        bool Disconnect(string username, Guid token);
    }
}
