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
                foreach (var tk in u.UserTokens)
                    Context.UserTokens.Remove(tk);
                foreach (var tk in u.UserRecoveryTokens)
                    Context.UserRecoveryTokens.Remove(tk);
                Context.Users.Remove(u);
            }
        }

        public void PurgeConnectionTokens()
        {
            foreach (var tk in Context.UserTokens.Where(x => x.Expiration < DateTime.Now))
                Context.UserTokens.Remove(tk);
            Context.SaveChanges();
        }

        public void PurgeRecoveryTokens()
        {
            foreach (var tk in Context.UserRecoveryTokens.Where(x => x.Expiration < DateTime.Now))
                Context.UserRecoveryTokens.Remove(tk);
            Context.SaveChanges();
        }
    }
}
