using Com.Ericmas001.Userbase.Entities;
using Com.Ericmas001.Userbase.Requests;
using Com.Ericmas001.Userbase.Responses;
using Com.Ericmas001.Userbase.Util;
using Com.Ericmas001.Userbase.ValidationTasks;

namespace Com.Ericmas001.Userbase.DbTasks
{
    public class CreateUserDbTask : AbstractDbTask
    {
        public CreateUserDbTask(UserbaseDbContext context) : base(context)
        {

        }
        public ConnectUserResponse CreateUser(CreateUserRequest request)
        {
            if (UserbaseSystem.UsernameExists(request.Username, Context))
                return InvalidResponse;

            if (UserbaseSystem.EmailExists(request.Authentication.Email, Context))
                return InvalidResponse;

            if (!new UsernameValidationTask().Validate(request.Username))
                return InvalidResponse;

            if (!new EmailValidationTask().Validate(request.Authentication.Email))
                return InvalidResponse;

            if (!new PasswordValidationTask().Validate(request.Authentication.Password))
                return InvalidResponse;

            if (!new DisplayNameValidationTask().Validate(request.Profile.DisplayName))
                return InvalidResponse;


            User u = new User
            {
                Name = request.Username,
                UserProfile = new UserProfile
                {
                    DisplayName = request.Profile.DisplayName
                },
                UserAuthentication = new UserAuthentication
                {
                    Password = BCrypt.EncryptPassword(request.Authentication.Password),
                    RecoveryEmail = request.Authentication.Email
                }
            };

            Context.Users.Add(u);
            Context.SaveChanges();

            return UserbaseSystem.ValidateCredentials(request.Username, request.Authentication.Password, Context);
        }

        private ConnectUserResponse InvalidResponse => new ConnectUserResponse
        {
            Token = null,
            Success = false
        };
    }
}
