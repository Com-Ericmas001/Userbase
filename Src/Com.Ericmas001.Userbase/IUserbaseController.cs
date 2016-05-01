using System;
using Com.Ericmas001.Userbase.Responses;

namespace Com.Ericmas001.Userbase
{
    public interface IUserbaseController
    {
        int IdFromUsername(UserbaseDbContext context, string username);
        int IdFromEmail(UserbaseDbContext context, string email);

        ConnectUserResponse ValidateToken(UserbaseDbContext context, string username, Guid token);
    }
}
