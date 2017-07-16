using System.Collections.Generic;
using Com.Ericmas001.Userbase.Models.Responses;

namespace Com.Ericmas001.Userbase.Models
{
    public class UserModel
    {
        public int IdUser { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public IEnumerable<GroupInfo> Groups { get; set; }
    }
}
