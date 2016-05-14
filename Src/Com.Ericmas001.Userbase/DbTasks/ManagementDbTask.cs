using System;
using System.Linq;

namespace Com.Ericmas001.Userbase.DbTasks
{
    public class ManagementDbTask : AbstractDbTask
    {
        public ManagementDbTask(UserbaseDbContext context) : base(context)
        {

        }
        public void PurgeUsers()
        {
            foreach (var u in Context.Users.Where(x => !x.Active).AsEnumerable().ToArray())
            {
                Context.UserProfiles.Remove(u.UserProfile);
                Context.UserAuthentications.Remove(u.UserAuthentication);
                Context.UserTokens.RemoveRange(u.UserTokens);
                Context.UserRecoveryTokens.RemoveRange(u.UserRecoveryTokens);
                Context.Users.Remove(u);
            }
        }

        public void PurgeConnectionTokens()
        {
            Context.UserTokens.RemoveRange(Context.UserTokens.Where(x => x.Expiration < DateTime.Now));
            Context.SaveChanges();
        }

        public void PurgeRecoveryTokens()
        {
            Context.UserRecoveryTokens.RemoveRange(Context.UserRecoveryTokens.Where(x => x.Expiration < DateTime.Now));
            Context.SaveChanges();
        }
    }
}
