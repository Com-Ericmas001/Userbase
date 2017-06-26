using System;
using Com.Ericmas001.Userbase.Requests.Models;

namespace Com.Ericmas001.Userbase.Requests
{
    public class AddUserToGroupRequest
    {
        public string Username { get; set; }
        public Guid Token { get; set; }
        public string UserToAdd { get; set; }
        public int IdGroup { get; set; }
    }
}
