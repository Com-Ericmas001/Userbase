using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Ericmas001.Userbase.Responses.Models;

namespace Com.Ericmas001.Userbase.Responses
{
    public class ConnectUserResponse
    {
        public bool Success { get; set; }

        public int IdUser { get; set; }

        public Token Token { get; set; }
    }
}
