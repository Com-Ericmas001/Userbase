using System;
using System.Linq;
using Com.Ericmas001.Userbase.Entities;
using Com.Ericmas001.Userbase.Responses;
using Com.Ericmas001.Userbase.Util;

namespace Com.Ericmas001.Userbase.DbTasks
{
    public class UserActivationDbTask : AbstractDbTask
    {
        public UserActivationDbTask(UserbaseDbContext context) : base(context)
        {

        }
        public bool Deactivate(string username, Guid token)
        {
            int idUser = UserbaseSystem.IdFromUsername(username, Context);

            //Doesn't exist
            if (idUser == 0)
                return false;

            UserToken ut = UserbaseUtil.GetConnectionTokenFromId(Context, idUser, token);
            if (ut == null)
                return false;

            User u = Context.Users.Single(x => x.IdUser == idUser);
            u.Active = false;
            Context.SaveChanges();

            return true;
        }
    }
}
