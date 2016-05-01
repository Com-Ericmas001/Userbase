using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ericmas001.Userbase
{
    public interface IUserbaseController
    {
        int IdFromUsername(UserbaseDbContext context, string username);
    }
}
