using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ericmas001.Userbase.ValidationTasks
{
    public class DisplayNameValidationTask : AbstractValidationTask<string>
    {
        public override bool Validate(string displayName)
        {
            if (displayName.Length < 3)
                return false;

            return true;
        }
    }
}
