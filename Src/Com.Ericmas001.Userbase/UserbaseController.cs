using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Ericmas001.Userbase.Entities;

namespace Com.Ericmas001.Userbase
{
    public class UserbaseController : IUserbaseController
    {
        public int IdFromUsername(UserbaseDbContext context, string username)
        {
            return context.Users.SingleOrDefault(x => x.Active && x.Name.Equals(username, StringComparison.InvariantCultureIgnoreCase))?.IdUser ?? 0;
        }
    }
}
