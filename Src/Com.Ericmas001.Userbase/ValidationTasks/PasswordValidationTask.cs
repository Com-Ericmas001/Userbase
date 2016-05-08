using System;
using System.Linq;

namespace Com.Ericmas001.Userbase.ValidationTasks
{
    public class PasswordValidationTask
    {
        public bool Validate(string password)
        {
            if (password.Length < 6)
                return false;

            if (!password.All(c => Char.IsLetterOrDigit(c) || Char.IsSymbol(c)))
                return false;

            return true;
        }
    }
}
