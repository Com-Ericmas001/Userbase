using System;
using Com.Ericmas001.Userbase.Models.Requests;
using Com.Ericmas001.Userbase.Models.Responses;

namespace Com.Ericmas001.Userbase.Models.ServiceInterfaces
{
    public interface IUserGroupingService
    {
        TokenSuccessResponse AddUserToGroup(AddUserToGroupRequest request);
        TokenSuccessResponse ExcludeUserFromGroup(string requestingUsername, Guid token, string userToExclude, int idGroup);
    }
}
