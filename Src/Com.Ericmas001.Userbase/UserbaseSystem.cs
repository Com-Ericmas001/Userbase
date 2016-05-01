using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
