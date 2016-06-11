using System.Linq;
using Com.Ericmas001.Userbase.Entities;
using Com.Ericmas001.Userbase.Requests;
using Com.Ericmas001.Userbase.Responses;
using Com.Ericmas001.Userbase.ValidationTasks;

namespace Com.Ericmas001.Userbase.DbTasks
{
    public class ModifyUserDbTask : AbstractDbTask
    {
        public ModifyUserDbTask(UserbaseDbContext context) : base(context)
        {

        }
        public TokenSuccessResponse ModifyProfile(ModifyProfileRequest request)
        {
            var connection = UserbaseSystem.ValidateToken(request.Username, request.Token, Context);

            if (!connection.Success)
                return InvalidResponse;

            User u = Context.Users.Single(x => x.IdUser == connection.IdUser);

            if (!string.IsNullOrEmpty(request.Profile.DisplayName))
            {
                if (!new DisplayNameValidationTask().Validate(request.Profile.DisplayName))
                    return InvalidResponse;
                u.UserProfile.DisplayName = request.Profile.DisplayName;
            }

            Context.SaveChanges();

            return connection;
        }
        public TokenSuccessResponse ModifyCredentials(ModifyCredentialsRequest request)
        {
            var connection = UserbaseSystem.ValidateToken(request.Username, request.Token, Context);

            if (!connection.Success)
                return InvalidResponse;

            User u = Context.Users.Single(x => x.IdUser == connection.IdUser);

            if (!string.IsNullOrEmpty(request.Authentication.Password))
            {
                if (!new PasswordValidationTask().Validate(request.Authentication.Password))
                    return InvalidResponse;
                u.UserAuthentication.Password = UserbaseSystem.EncryptPassword(request.Authentication.Password);
            }

            if (!string.IsNullOrEmpty(request.Authentication.Email))
            {
                if (!new EmailValidationTask().Validate(request.Authentication.Email))
                    return InvalidResponse;

                if (UserbaseSystem.EmailExists(request.Authentication.Email, Context))
                    return InvalidResponse;

                u.UserAuthentication.RecoveryEmail = request.Authentication.Email;
            }

            Context.SaveChanges();

            return connection;
        }

        private TokenSuccessResponse InvalidResponse => new TokenSuccessResponse
        {
            Token = null,
            Success = false
        };
    }
}
