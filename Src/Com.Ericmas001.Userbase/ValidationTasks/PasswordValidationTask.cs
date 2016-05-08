using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ericmas001.Userbase.ValidationTasks
{
    public class PasswordValidationTask : AbstractValidationTask<string>
    {
        public override bool Validate(string password)
        {
            if (password.Length < 6)
                return false;

            if (!password.All(c => Char.IsLetterOrDigit(c) || Char.IsSymbol(c)))
                return false;

            return true;
        }
    }
}
