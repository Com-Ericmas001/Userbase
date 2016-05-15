using System;
using Com.Ericmas001.Userbase.DbTasks;
using Com.Ericmas001.Userbase.DbTasks.Models;
using Com.Ericmas001.Userbase.Requests;
using Com.Ericmas001.Userbase.Responses;

namespace Com.Ericmas001.Userbase
{
    public class UserbaseController : IUserbaseController
    {
        public int IdFromUsername(UserbaseDbContext context, string username)
        {
            return new ObtainUserIdDbTask(context).FromUsername(username);
        }
        public int IdFromEmail(UserbaseDbContext context, string email)
        {
            return new ObtainUserIdDbTask(context).FromEmail(email);
        }

        public ConnectUserResponse ValidateToken(UserbaseDbContext context, string username, Guid token)
        {
            return new ConnectUserDbTask(context).WithToken(username, token);
        }

        public ConnectUserResponse ValidateCredentials(UserbaseDbContext context, string username, string password)
        {
            return new ConnectUserDbTask(context).WithPassword(username, password);
        }

        public ConnectUserResponse CreateUser(UserbaseDbContext context, CreateUserRequest request)
        {
            return new CreateUserDbTask(context).CreateUser(request);
        }

        public TokenSuccessResponse ModifyCredentials(UserbaseDbContext context, ModifyCredentialsRequest request)
        {
            return new ModifyUserDbTask(context).ModifyCredentials(request);
        }

        public TokenSuccessResponse ModifyProfile(UserbaseDbContext context, ModifyProfileRequest request)
        {
            return new ModifyUserDbTask(context).ModifyProfile(request);
        }

        public bool Disconnect(UserbaseDbContext context, string username, Guid token)
        {
            return new ConnectUserDbTask(context).Disconnect(username, token);
        }

        public void PurgeUsers(UserbaseDbContext context)
        {
            new ManagementDbTask(context).PurgeUsers();
        }

        public void PurgeConnectionTokens(UserbaseDbContext context)
        {
            new ManagementDbTask(context).PurgeConnectionTokens();
        }

        public void PurgeRecoveryTokens(UserbaseDbContext context)
        {
            new ManagementDbTask(context).PurgeRecoveryTokens();
        }

        public bool Deactivate(UserbaseDbContext context, string username, Guid token)
        {
            return new UserActivationDbTask(context).Deactivate(username, token);
        }

        public bool SendRecoveryToken(UserbaseDbContext context, string username, IEmailSender smtp)
        {
            return new PasswordRecoveryDbTask(context).SendRecoveryToken(username, smtp);
        }

        public ConnectUserResponse ResetPassword(UserbaseDbContext context, string username, Guid recoveryToken, string newPassword)
        {
            return new PasswordRecoveryDbTask(context).ResetPassword(username, recoveryToken, newPassword);
        }

        public UserSummaryResponse UserSummary(UserbaseDbContext context, string askingUser, Guid token, string requestedUser)
        {
            return new UserSummaryDbTask(context).Summary(askingUser, token, requestedUser);
        }
    }
}
