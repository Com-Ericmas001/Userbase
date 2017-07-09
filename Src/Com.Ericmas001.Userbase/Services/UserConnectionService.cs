using System;
using System.Linq;
using Com.Ericmas001.Security.Cryptography;
using Com.Ericmas001.Userbase.Entities;
using Com.Ericmas001.Userbase.Models;
using Com.Ericmas001.Userbase.Models.Responses;
using Com.Ericmas001.Userbase.Services.Interfaces;

namespace Com.Ericmas001.Userbase.Services
{
    public class UserConnectionService : IUserConnectionService
    {
        private readonly IUserbaseDbContext m_DbContext;
        private readonly IUserbaseConfig m_Config;
        private readonly IUserObtentionService m_UserObtentionService;

        public UserConnectionService(IUserbaseDbContext dbContext, IUserbaseConfig config, IUserObtentionService userObtentionService)
        {
            m_DbContext = dbContext;
            m_Config = config;
            m_UserObtentionService = userObtentionService;
        }

        public ConnectUserResponse ConnectWithToken(string username, Guid token)
        {
            int idUser = m_UserObtentionService.FromUsername(username);

            //Doesn't exist
            if (idUser == 0)
                return new ConnectUserResponse { Success = false };

            //Invalid token
            var tok = ValidateToken(idUser, token);
            if (tok == null)
                return new ConnectUserResponse { Success = false, IdUser = idUser };

            return new ConnectUserResponse { Success = true, IdUser = idUser, Token = tok };
        }

        public ConnectUserResponse ConnectWithPassword(string username, string password)
        {
            int idUser = m_UserObtentionService.FromUsername(username);

            //Doesn't exist
            if (idUser == 0)
                return new ConnectUserResponse { Success = false };

            //Invalid Password
            if (!BCrypt.CheckPassword(m_Config.SaltPassword(password), m_DbContext.UserAuthentications.Single(x => x.IdUser == idUser).Password))
                return new ConnectUserResponse { Success = false };

            return new ConnectUserResponse { Success = true, IdUser = idUser, Token = CreateToken(idUser) };
        }

        public bool Disconnect(string username, Guid token)
        {
            int idUser = m_UserObtentionService.FromUsername(username);

            //Doesn't exist
            if (idUser == 0)
                return false;

            //Invalid token
            UserToken ut = UserToken.FromId(m_DbContext, idUser, token);
            if (ut == null)
                return false;

            ut.Expiration = DateTime.Now.AddSeconds(-1);
            m_DbContext.SaveChanges();

            return true;
        }

        private ConnectionToken CreateToken(int idUser)
        {
            var token = new ConnectionToken();
            m_DbContext.UserTokens.Add(new UserToken { Token = token.Id, Expiration = token.ValidUntil, IdUser = idUser });
            m_DbContext.SaveChanges();
            return token;
        }

        private ConnectionToken ValidateToken(int idUser, Guid token)
        {
            UserToken ut = UserToken.FromId(m_DbContext, idUser, token);
            if (ut == null)
                return null;
            ut.Expiration = ConnectionToken.NextExpiration;
            m_DbContext.SaveChanges();
            return new ConnectionToken(ut.Token, ut.Expiration);
        }
    }
}
