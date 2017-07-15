using System.Collections.Generic;

namespace Com.Ericmas001.Userbase.Models
{
    public class UserModel
    {
        public int IdUser { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public Dictionary<int,string> Groups { get; set; }
    }
}
