using System;
using Com.Ericmas001.Userbase.Models.Responses;

namespace Com.Ericmas001.Userbase.Models.ServiceInterfaces
{
    public interface IUserInformationService
    {
        ListUsersResponse ListAllUsers(string askingUser, Guid token);
        UserSummaryResponse UserSummary(string askingUser, Guid token, string requestedUser);
    }
}
