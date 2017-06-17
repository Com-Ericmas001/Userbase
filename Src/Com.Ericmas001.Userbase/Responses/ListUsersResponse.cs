using System.Collections.Generic;
using Com.Ericmas001.Userbase.Responses.Models;

namespace Com.Ericmas001.Userbase.Responses
{
    public class ListUsersResponse : TokenSuccessResponse
    {
        public IEnumerable<UserModel> Users { get; set; }
    }
}
