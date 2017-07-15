using System;

namespace Com.Ericmas001.Userbase.Models.Requests
{
    public class AddUserToGroupRequest
    {
        public string Username { get; set; }
        public Guid Token { get; set; }
        public string UserToAdd { get; set; }
        public int IdGroup { get; set; }
    }
}
