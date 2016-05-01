using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Ericmas001.Userbase.Entities;
using Com.Ericmas001.Userbase.Responses;
using Com.Ericmas001.Userbase.Responses.Models;

namespace Com.Ericmas001.Userbase.DbTasks
{
    public class ConnectUserDbTask : AbstractDbTask
    {
        public ConnectUserDbTask(UserbaseDbContext context) : base(context)
        {

        }
        public ConnectUserResponse WithToken(string username, Guid token)
        {

            var toks = Context.UserTokens.ToArray();

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
            throw new NotImplementedException();
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
    }
}
