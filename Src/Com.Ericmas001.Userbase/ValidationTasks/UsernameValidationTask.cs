using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ericmas001.Userbase.ValidationTasks
{
    public class UsernameValidationTask : AbstractValidationTask<string>
    {
        public override bool Validate(string username)
        {
            if (username.Length < 3)
                return false;

            if (!username.All(Char.IsLetterOrDigit))
                return false;

            return true;
        }
    }
}
