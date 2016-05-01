using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ericmas001.Userbase.DbTasks
{
    public class ObtainUserIdDbTask : AbstractDbTask
    {
        public ObtainUserIdDbTask(UserbaseDbContext context) : base(context)
        {

        }
        public int FromUsername(string username)
        {
            return Context.Users.SingleOrDefault(x => x.Active && x.Name.Equals(username, StringComparison.InvariantCultureIgnoreCase))?.IdUser ?? 0;
        }
        public int FromEmail(string email)
        {
            return Context.Users.SingleOrDefault(x => x.Active && x.UserAuthentication.RecoveryEmail.Equals(email, StringComparison.InvariantCultureIgnoreCase))?.IdUser ?? 0;
        }
    }
}
