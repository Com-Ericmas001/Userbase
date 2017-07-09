using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Ericmas001.Userbase.Models.Requests;
using Com.Ericmas001.Userbase.Models.Responses;

namespace Com.Ericmas001.Userbase.Services.Interfaces
{
    public interface IUserGroupingService
    {
        TokenSuccessResponse AddUserToGroup(AddUserToGroupRequest request);
        TokenSuccessResponse ExcludeUserFromGroup(string requestingUsername, Guid token, string userToExclude, int idGroup);
    }
}
