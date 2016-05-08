using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ericmas001.Userbase.Entities
{
    public interface IEntityWithId
    {
        int Id { get; set; }
    }
}
