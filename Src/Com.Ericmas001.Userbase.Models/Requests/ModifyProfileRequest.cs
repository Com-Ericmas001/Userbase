using System;

namespace Com.Ericmas001.Userbase.Models.Requests
{
    public class ModifyProfileRequest
    {
        public string Username { get; set; }
        public Guid Token { get; set; }
        public ProfileInfo Profile { get; set; }
    }
}
