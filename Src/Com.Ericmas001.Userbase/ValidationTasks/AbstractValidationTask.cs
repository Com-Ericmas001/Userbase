using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ericmas001.Userbase.ValidationTasks
{
    public abstract class AbstractValidationTask<T>
    {
        public abstract bool Validate(T item);
    }
}
