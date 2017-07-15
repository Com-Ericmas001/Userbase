using System;

namespace Com.Ericmas001.Userbase.Models.Requests
{
    public class ModifyCredentialsRequest
    {
        public string Username { get; set; }
        public Guid Token { get; set; }
        public AuthenticationInfo Authentication { get; set; }
    }
}
