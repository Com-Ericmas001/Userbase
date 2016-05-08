using System;

namespace Com.Ericmas001.Userbase
{
    public class NotInitializedException : Exception
    {
        public NotInitializedException()
            : base("UserbaseSystem not initialized, call UserbaseSystem.Init() before anything else !")
        {
            
        }
    }
}
