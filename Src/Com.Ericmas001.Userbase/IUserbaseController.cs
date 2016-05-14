using System;
using Com.Ericmas001.Userbase.Requests;
using Com.Ericmas001.Userbase.Responses;

namespace Com.Ericmas001.Userbase
{
    public interface IUserbaseController
    {
        int IdFromUsername(UserbaseDbContext context, string username);
        int IdFromEmail(UserbaseDbContext context, string email);
        ConnectUserResponse ValidateToken(UserbaseDbContext context, string username, Guid token);
        ConnectUserResponse ValidateCredentials(UserbaseDbContext context, string username, string password);
        ConnectUserResponse CreateUser(UserbaseDbContext context, CreateUserRequest request);
        TokenSuccessResponse ModifyCredentials(UserbaseDbContext context, ModifyCredentialsRequest request);
        TokenSuccessResponse ModifyProfile(UserbaseDbContext context, ModifyProfileRequest request);
        bool Disconnect(UserbaseDbContext context, string username, Guid token);
        void PurgeUsers(UserbaseDbContext context);
        void PurgeConnectionTokens(UserbaseDbContext context);
        void PurgeRecoveryTokens(UserbaseDbContext context);
    }
}
