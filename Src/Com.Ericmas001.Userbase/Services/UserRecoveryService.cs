using System;
using System.Linq;
using Com.Ericmas001.Userbase.Entities;
using Com.Ericmas001.Userbase.Models;
using Com.Ericmas001.Userbase.Models.Responses;
using Com.Ericmas001.Userbase.Models.ServiceInterfaces;

namespace Com.Ericmas001.Userbase.Services
{
    public class UserRecoveryService : IUserRecoveryService
    {
        private readonly IUserbaseDbContext m_DbContext;
        private readonly ISecurityService m_SecurityService;
        private readonly IUserObtentionService m_UserObtentionService;
        private readonly IUserConnectionService m_UserConnectionService;
        private readonly ISendEmailService m_SendEmailService;

        public UserRecoveryService(IUserbaseDbContext dbContext, ISecurityService securityService, IUserObtentionService userObtentionService, IUserConnectionService userConnectionService, ISendEmailService sendEmailService)
        {
            m_DbContext = dbContext;
            m_SecurityService = securityService;
            m_UserObtentionService = userObtentionService;
            m_UserConnectionService = userConnectionService;
            m_SendEmailService = sendEmailService;
        }

        public bool SendRecoveryToken(string username)
        {
            int idUser = m_UserObtentionService.FromUsername(username);

            //Doesn't exist
            if (idUser == 0)
                return false;

            User u = m_DbContext.Users.Single(x => x.IdUser == idUser);

            var token = new RecoveryToken();
            u.UserRecoveryTokens.Add(new UserRecoveryToken { Token = token.Id, Expiration = token.ValidUntil });
            m_DbContext.SaveChanges();

            m_SendEmailService.SendRecoveryToken(token, username, u.UserAuthentication.RecoveryEmail);

            return true;
        }

        public ConnectUserResponse ResetPassword(string username, string recoveryToken, string newPassword)
        {
            int idUser = m_UserObtentionService.FromUsername(username);

            //Doesn't exist
            if (idUser == 0)
                return InvalidResponse;

            UserRecoveryToken urt = UserRecoveryToken.FromId(m_DbContext, idUser, recoveryToken);
            if (urt == null)
                return InvalidResponse;

            if (!new ValidationService().ValidatePassword(newPassword))
                return InvalidResponse;

            User u = m_DbContext.Users.Single(x => x.IdUser == idUser);

            urt.Expiration = DateTime.Now.AddSeconds(-1);
            u.UserAuthentication.Password = m_SecurityService.EncryptPassword(newPassword);
            m_DbContext.SaveChanges();

            return m_UserConnectionService.ConnectWithPassword(username, newPassword);
        }
        private ConnectUserResponse InvalidResponse => new ConnectUserResponse
        {
            Token = null,
            Success = false
        };
    }
}
