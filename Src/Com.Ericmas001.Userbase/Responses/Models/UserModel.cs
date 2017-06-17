using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ericmas001.Userbase.Responses.Models
{
    public class UserModel
    {
        public int IdUser { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public Dictionary<int,string> Groups { get; set; }
    }
}
