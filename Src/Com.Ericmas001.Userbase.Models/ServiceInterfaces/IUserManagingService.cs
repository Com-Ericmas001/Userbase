using System;
using Com.Ericmas001.Userbase.Models.Requests;
using Com.Ericmas001.Userbase.Models.Responses;

namespace Com.Ericmas001.Userbase.Models.ServiceInterfaces
{
    public interface IUserManagingService
    {
        ConnectUserResponse CreateUser(CreateUserRequest request);
        TokenSuccessResponse ModifyProfile(ModifyProfileRequest request);
        TokenSuccessResponse ModifyCredentials(ModifyCredentialsRequest request);
        bool Deactivate(string username, Guid token);
    }
}
