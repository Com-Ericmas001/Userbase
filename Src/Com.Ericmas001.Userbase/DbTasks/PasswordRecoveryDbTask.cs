using System;
using System.Linq;
using Com.Ericmas001.Userbase.DbTasks.Models;
using Com.Ericmas001.Userbase.Entities;
using Com.Ericmas001.Userbase.Responses;
using Com.Ericmas001.Userbase.Responses.Models;
using Com.Ericmas001.Userbase.Util;
using Com.Ericmas001.Userbase.ValidationTasks;

namespace Com.Ericmas001.Userbase.DbTasks
{
    public class PasswordRecoveryDbTask : AbstractDbTask
    {
        public PasswordRecoveryDbTask(UserbaseDbContext context) : base(context)
        {

        }

        public bool SendRecoveryToken(string username, IEmailSender smtp)
        {
            int idUser = UserbaseSystem.IdFromUsername(username, Context);

            //Doesn't exist
            if (idUser == 0)
                return false;

            User u = Context.Users.Single(x => x.IdUser == idUser);

            var token = new RecoveryToken();
            u.UserRecoveryTokens.Add(new UserRecoveryToken { Token = token.Id, Expiration = token.ValidUntil });
            Context.SaveChanges();

            smtp.SendToken(token, u.UserAuthentication.RecoveryEmail);

            return true;
        }

        public ConnectUserResponse ResetPassword(string username, Guid recoveryToken, string newPassword)
        {
            int idUser = UserbaseSystem.IdFromUsername(username, Context);

            //Doesn't exist
            if (idUser == 0)
                return InvalidResponse;

            UserRecoveryToken urt = UserRecoveryToken.FromId(Context, idUser, recoveryToken);
            if (urt == null)
                return InvalidResponse;

            if (!new PasswordValidationTask().Validate(newPassword))
                return InvalidResponse;

            User u = Context.Users.Single(x => x.IdUser == idUser);

            urt.Expiration = DateTime.Now.AddSeconds(-1);
            u.UserAuthentication.Password = BCrypt.EncryptPassword(newPassword);
            Context.SaveChanges();

            return UserbaseSystem.ValidateCredentials(username, newPassword, Context);
        }

        private ConnectUserResponse InvalidResponse => new ConnectUserResponse
        {
            Token = null,
            Success = false
        };
    }
}
