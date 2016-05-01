using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Ericmas001.Userbase.Entities;
using Com.Ericmas001.Userbase.Responses;
using Com.Ericmas001.Userbase.Responses.Models;
using Com.Ericmas001.Userbase.Util;

namespace Com.Ericmas001.Userbase.DbTasks
{
    public class ConnectUserDbTask : AbstractDbTask
    {
        public ConnectUserDbTask(UserbaseDbContext context) : base(context)
        {

        }
        public ConnectUserResponse WithToken(string username, Guid token)
        {
            int idUser = UserbaseSystem.Controller.IdFromUsername(Context, username);

            //Doesn't exist
            if (idUser == 0)
                return new ConnectUserResponse { Success = false };

            //Invalid token
            var tok = TokenIsValid(idUser, token);
            if (tok == null)
                return new ConnectUserResponse { Success = false, IdUser = idUser };

            return new ConnectUserResponse { Success = true, IdUser = idUser, Token = tok };
        }
        public ConnectUserResponse WithPassword(string username, string password)
        {
            int idUser = UserbaseSystem.Controller.IdFromUsername(Context, username);

            //Doesn't exist
            if (idUser == 0)
                return new ConnectUserResponse { Success = false };

            //Invalid Password
            if (!BCrypt.CheckPassword(UserbaseSystem.SaltPassword(password), Context.UserAuthentications.Single(x => x.IdUser == idUser).Password))
                return new ConnectUserResponse { Success = false };
            
            return new ConnectUserResponse { Success = true, IdUser = idUser, Token = CreateToken(idUser) };
        }

        private ConnectionToken TokenIsValid(int idUser, Guid token)
        {
            UserToken ut = GetTokenFromId(idUser, token);
            if (ut == null)
                return null;
            ut.Expiration = ConnectionToken.NextExpiration;
            Context.SaveChanges();
            return new ConnectionToken(ut.Token, ut.Expiration);
        }
        private UserToken GetTokenFromId(int idUser, Guid token)
        {
            return Context.UserTokens.SingleOrDefault(t => t.IdUser == idUser && t.Token == token && t.Expiration > DateTime.Now);
        }

        private Token CreateToken(int idUser)
        {
            var token = new ConnectionToken();
            Context.UserTokens.Add(new UserToken { Token = token.Id, Expiration = token.ValidUntil, IdUser = idUser});
            Context.SaveChanges();
            return token;
        }
    }
}
