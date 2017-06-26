using System;
using System.Linq;
using Com.Ericmas001.Security.Cryptography;
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
            int idUser = UserbaseSystem.IdFromUsername(username, Context);

            //Doesn't exist
            if (idUser == 0)
                return new ConnectUserResponse { Success = false };

            //Invalid token
            var tok = ConnectionTokenIsValid(idUser, token);
            if (tok == null)
                return new ConnectUserResponse { Success = false, IdUser = idUser };

            return new ConnectUserResponse { Success = true, IdUser = idUser, Token = tok };
        }
        public ConnectUserResponse WithPassword(string username, string password)
        {
            int idUser = UserbaseSystem.IdFromUsername(username, Context);

            //Doesn't exist
            if (idUser == 0)
                return new ConnectUserResponse { Success = false };

            //Invalid Password
            if (!BCrypt.CheckPassword(UserbaseSystem.SaltPassword(password), Context.UserAuthentications.Single(x => x.IdUser == idUser).Password))
                return new ConnectUserResponse { Success = false };

            return new ConnectUserResponse { Success = true, IdUser = idUser, Token = CreateConnectionToken(idUser) };
        }
        public bool Disconnect(string username, Guid token)
        {
            int idUser = UserbaseSystem.IdFromUsername(username, Context);

            //Doesn't exist
            if (idUser == 0)
                return false;

            //Invalid token
            UserToken ut = UserToken.FromId(Context, idUser, token);
            if (ut == null)
                return false;

            ut.Expiration = DateTime.Now.AddSeconds(-1);
            Context.SaveChanges();

            return true;
        }

        private ConnectionToken CreateConnectionToken(int idUser)
        {
            var token = new ConnectionToken();
            Context.UserTokens.Add(new UserToken { Token = token.Id, Expiration = token.ValidUntil, IdUser = idUser });
            Context.SaveChanges();
            return token;
        }

        private ConnectionToken ConnectionTokenIsValid(int idUser, Guid token)
        {
            UserToken ut = UserToken.FromId(Context, idUser, token);
            if (ut == null)
                return null;
            ut.Expiration = ConnectionToken.NextExpiration;
            Context.SaveChanges();
            return new ConnectionToken(ut.Token, ut.Expiration);
        }
    }
}
