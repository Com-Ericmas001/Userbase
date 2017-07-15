using System.Collections.Generic;

namespace Com.Ericmas001.Userbase.Models.Responses
{
    public class ListUsersResponse : TokenSuccessResponse
    {
        public IEnumerable<UserModel> Users { get; set; }
    }
}
