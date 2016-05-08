using System;
using Com.Ericmas001.Userbase.Requests.Models;

namespace Com.Ericmas001.Userbase.Requests
{
    public class ModifyCredentialsRequest
    {
        public string Username { get; set; }
        public Guid Token { get; set; }
        public AuthenticationInfo Authentication { get; set; }
    }
}
