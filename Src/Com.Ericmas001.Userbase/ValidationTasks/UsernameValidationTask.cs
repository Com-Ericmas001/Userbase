using System;
using System.Linq;

namespace Com.Ericmas001.Userbase.ValidationTasks
{
    public class UsernameValidationTask
    {
        public bool Validate(string username)
        {
            if (username.Length < 3)
                return false;

            if (!username.All(Char.IsLetterOrDigit))
                return false;

            return true;
        }
    }
}
