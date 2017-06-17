using System;
using Com.Ericmas001.Security.Cryptography;
using Com.Ericmas001.Userbase.DbTasks.Models;
using Com.Ericmas001.Userbase.Requests;
using Com.Ericmas001.Userbase.Responses;

namespace Com.Ericmas001.Userbase
{
    public static class UserbaseSystem
    {
        private static bool m_Initialized;

        private static Func<UserbaseDbContext> m_ContextGenerator;

        private static string m_Salt;

        private static IUserbaseController m_Controller;

        private static IUserbaseController Controller
        {
            get
            {
                if (!m_Initialized)
                    throw new NotInitializedException();
                return m_Controller;
            }
            set { m_Controller = value; }
        }

        internal static string SaltPassword(string unsaltedPassword)
        {
            return $"{m_Salt}{unsaltedPassword}";

        }

        public static void Init(string salt, IUserbaseController controller = null, Func<UserbaseDbContext> contextGenerator = null, string connString = null)
        {
            Controller = controller ?? new UserbaseController();
            m_Salt = salt;
            m_ContextGenerator = contextGenerator ?? (() => connString == null ? new UserbaseDbContext() : new UserbaseDbContext(connString));
            m_Initialized = true;
        }

        private static TResponse Execute<TResponse>(Func<UserbaseDbContext, TResponse> thingToDo, UserbaseDbContext existingContext)
        {
            if (existingContext != null)
                return thingToDo(existingContext);

            using (var context = m_ContextGenerator.Invoke())
                return thingToDo(context);
        }
        private static void Execute(Action<UserbaseDbContext> thingToDo, UserbaseDbContext existingContext)
        {
            if (existingContext != null)
                thingToDo(existingContext);
            else
            {
                using (var context = m_ContextGenerator.Invoke())
                    thingToDo(context);
            }
        }

        public static string EncryptPassword(string password)
        {
            return BCrypt.HashPassword(SaltPassword(password), BCrypt.GenerateSalt());
        }

        public static int IdFromUsername(string username, UserbaseDbContext existingContext = null)
        {
            return Execute(context => Controller.IdFromUsername(context, username), existingContext);
        }

        public static bool UsernameExists(string username, UserbaseDbContext existingContext = null)
        {
            return IdFromUsername(username, existingContext) != 0;
        }

        public static int IdFromEmail(string email, UserbaseDbContext existingContext = null)
        {
            return Execute(context => Controller.IdFromEmail(context, email), existingContext);
        }

        public static bool EmailExists(string email, UserbaseDbContext existingContext = null)
        {
            return IdFromEmail(email, existingContext) != 0;
        }

        public static ConnectUserResponse ValidateCredentials(string username, string password, UserbaseDbContext existingContext = null)
        {
            return Execute(context => Controller.ValidateCredentials(context, username, password), existingContext);
        }

        public static ConnectUserResponse ValidateToken(string username, Guid token, UserbaseDbContext existingContext = null)
        {
            return Execute(context => Controller.ValidateToken(context, username, token), existingContext);
        }

        public static ConnectUserResponse CreateUser(CreateUserRequest request, UserbaseDbContext existingContext = null)
        {
            return Execute(context => Controller.CreateUser(context, request), existingContext);
        }

        public static TokenSuccessResponse ModifyCredentials(ModifyCredentialsRequest request, UserbaseDbContext existingContext = null)
        {
            return Execute(context => Controller.ModifyCredentials(context, request), existingContext);
        }

        public static TokenSuccessResponse ModifyProfile(ModifyProfileRequest request, UserbaseDbContext existingContext = null)
        {
            return Execute(context => Controller.ModifyProfile(context, request), existingContext);
        }

        public static bool Disconnect(string username, Guid token, UserbaseDbContext existingContext = null)
        {
            return Execute(context => Controller.Disconnect(context, username, token), existingContext);
        }

        public static void PurgeUsers(UserbaseDbContext existingContext = null)
        {
            Execute(context => Controller.PurgeUsers(context), existingContext);
        }

        public static void PurgeConnectionTokens(UserbaseDbContext existingContext = null)
        {
            Execute(context => Controller.PurgeConnectionTokens(context), existingContext);
        }

        public static void PurgeRecoveryTokens(UserbaseDbContext existingContext = null)
        {
            Execute(context => Controller.PurgeRecoveryTokens(context), existingContext);
        }

        public static bool Deactivate(string username, Guid token, UserbaseDbContext existingContext = null)
        {
            return Execute(context => Controller.Deactivate(context, username, token), existingContext);
        }

        public static bool SendRecoveryToken(string username, IEmailSender smtp, UserbaseDbContext existingContext = null)
        {
            return Execute(context => Controller.SendRecoveryToken(context, username, smtp), existingContext);
        }

        public static ConnectUserResponse ResetPassword(string username, Guid recoveryToken, string newPassword, UserbaseDbContext existingContext = null)
        {
            return Execute(context => Controller.ResetPassword(context, username, recoveryToken, newPassword), existingContext);
        }

        public static UserSummaryResponse UserSummary(string askingUser, Guid token, string requestedUser, UserbaseDbContext existingContext = null)
        {
            return Execute(context => Controller.UserSummary(context, askingUser, token, requestedUser), existingContext);
        }

        public static ListUsersResponse ListUsers(string username, Guid token, UserbaseDbContext existingContext = null)
        {
            return Execute(context => Controller.ListUsers(context, username, token), existingContext);
        }
    }
}
