using System;
using System.Linq;
using Com.Ericmas001.Userbase.Responses;
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
            int idUser = UserbaseSystem.IdFromUsername(username, Context);

            //Doesn't exist
            if (idUser == 0)
                return new ConnectUserResponse { Success = false };

            //Invalid token
            var tok = UserbaseUtil.ConnectionTokenIsValid(Context,idUser, token);
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
            
            return new ConnectUserResponse { Success = true, IdUser = idUser, Token = UserbaseUtil.CreateConnectionToken(Context, idUser) };
        }
    }
}
