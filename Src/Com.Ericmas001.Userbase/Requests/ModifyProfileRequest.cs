using System;
using Com.Ericmas001.Userbase.Requests.Models;

namespace Com.Ericmas001.Userbase.Requests
{
    public class ModifyProfileRequest
    {
        public string Username { get; set; }
        public Guid Token { get; set; }
        public ProfileInfo Profile { get; set; }
    }
}
