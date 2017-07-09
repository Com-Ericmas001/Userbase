using System;
using System.Linq;
using Com.Ericmas001.Userbase.Services.Interfaces;

namespace Com.Ericmas001.Userbase.Services
{
    public class ManagementService : IManagementService
    {
        private readonly IUserbaseDbContext m_DbContext;

        public ManagementService(IUserbaseDbContext dbContext)
        {
            m_DbContext = dbContext;
        }
        public void PurgeUsers()
        {
            foreach (var u in m_DbContext.Users.Where(x => !x.Active).AsEnumerable().ToArray())
            {
                m_DbContext.UserProfiles.Remove(u.UserProfile);
                m_DbContext.UserAuthentications.Remove(u.UserAuthentication);
                foreach (var tk in u.UserTokens)
                    m_DbContext.UserTokens.Remove(tk);
                foreach (var tk in u.UserRecoveryTokens)
                    m_DbContext.UserRecoveryTokens.Remove(tk);
                m_DbContext.Users.Remove(u);
            }
        }
        public void PurgeConnectionTokens()
        {
            foreach (var tk in m_DbContext.UserTokens.Where(x => x.Expiration < DateTime.Now))
                m_DbContext.UserTokens.Remove(tk);
            m_DbContext.SaveChanges();
        }
        public void PurgeRecoveryTokens()
        {
            foreach (var tk in m_DbContext.UserRecoveryTokens.Where(x => x.Expiration < DateTime.Now))
                m_DbContext.UserRecoveryTokens.Remove(tk);
            m_DbContext.SaveChanges();
        }
    }
}
