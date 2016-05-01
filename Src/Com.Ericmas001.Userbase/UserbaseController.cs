using System;
using Com.Ericmas001.Userbase.DbTasks;
using Com.Ericmas001.Userbase.Responses;

namespace Com.Ericmas001.Userbase
{
    public class UserbaseController : IUserbaseController
    {
        public int IdFromUsername(UserbaseDbContext context, string username)
        {
            return new ObtainUserIdDbTask(context).FromUsername(username);
        }
        public int IdFromEmail(UserbaseDbContext context, string email)
        {
            return new ObtainUserIdDbTask(context).FromEmail(email);
        }

        public ConnectUserResponse ValidateToken(UserbaseDbContext context, string username, Guid token)
        {
            return new ConnectUserDbTask(context).WithToken(username, token);
        }
    }
}
