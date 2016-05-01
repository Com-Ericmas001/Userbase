using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Ericmas001.Userbase.Responses;
using Com.Ericmas001.Userbase.Responses.Models;

namespace Com.Ericmas001.Userbase
{
    public static class UserbaseSystem
    {
        private static bool m_Initialized;

        private static Func<UserbaseDbContext> m_ContextGenerator;

        private static string m_Salt;

        private static IUserbaseController m_Controller;

        internal static IUserbaseController Controller
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

        public static int IdFromUsername(string username)
        {
            using (var context = m_ContextGenerator.Invoke())
                return Controller.IdFromUsername(context, username);
        }

        public static bool UsernameExists(string username)
        {
            return IdFromUsername(username) != 0;
        }

        public static int IdFromEmail(string email)
        {
            using (var context = m_ContextGenerator.Invoke())
                return Controller.IdFromEmail(context, email);
        }

        public static bool EmailExists(string email)
        {
            return IdFromEmail(email) != 0;
        }

        public static ConnectUserResponse ValidateCredentials(string username, string password)
        {
            using (var context = m_ContextGenerator.Invoke())
                return Controller.ValidateCredentials(context, username, password);
        }

        public static ConnectUserResponse ValidateToken(string username, Guid token)
        {
            using (var context = m_ContextGenerator.Invoke())
                return Controller.ValidateToken(context, username, token);
        }
    }
}
