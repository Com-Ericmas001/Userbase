using System;
using Com.Ericmas001.Userbase.Models.Responses;

namespace Com.Ericmas001.Userbase.Models.ServiceInterfaces
{
    public interface IUserConnectionService
    {
        ConnectUserResponse ConnectWithToken(string username, Guid token);
        ConnectUserResponse ConnectWithPassword(string username, string password);
        bool Disconnect(string username, Guid token);
    }
}
