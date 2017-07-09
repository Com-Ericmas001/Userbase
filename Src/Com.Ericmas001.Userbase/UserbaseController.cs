using System;
using Com.Ericmas001.Userbase.Models;
using Com.Ericmas001.Userbase.Models.Requests;
using Com.Ericmas001.Userbase.Models.Responses;
using Com.Ericmas001.Userbase.Services;

namespace Com.Ericmas001.Userbase
{
    public class UserbaseController : IUserbaseController
    {
        public int IdFromUsername(UserbaseDbContext context, string username)
        {
            return new UserObtentionService(context).FromUsername(username);
        }
        public int IdFromEmail(UserbaseDbContext context, string email)
        {
            return new UserObtentionService(context).FromEmail(email);
        }

        public ConnectUserResponse ValidateToken(UserbaseDbContext context, string username, Guid token)
        {
            return new UserConnectionService(context, UserbaseSystem.Config, new UserObtentionService(context) ).ConnectWithToken(username, token);
        }

        public ConnectUserResponse ValidateCredentials(UserbaseDbContext context, string username, string password)
        {
            return new UserConnectionService(context, UserbaseSystem.Config, new UserObtentionService(context)).ConnectWithPassword(username, password);
        }

        public ConnectUserResponse CreateUser(UserbaseDbContext context, CreateUserRequest request)
        {
            return new UserManagingService(context, new UserObtentionService(context), new UserConnectionService(context, UserbaseSystem.Config, new UserObtentionService(context)), new ValidationService()).CreateUser(request);
        }

        public TokenSuccessResponse ModifyCredentials(UserbaseDbContext context, ModifyCredentialsRequest request)
        {
            return new UserManagingService(context, new UserObtentionService(context), new UserConnectionService(context, UserbaseSystem.Config, new UserObtentionService(context)), new ValidationService()).ModifyCredentials(request);
        }

        public TokenSuccessResponse ModifyProfile(UserbaseDbContext context, ModifyProfileRequest request)
        {
            return new UserManagingService(context, new UserObtentionService(context), new UserConnectionService(context, UserbaseSystem.Config, new UserObtentionService(context)), new ValidationService()).ModifyProfile(request);
        }

        public bool Disconnect(UserbaseDbContext context, string username, Guid token)
        {
            return new UserConnectionService(context, UserbaseSystem.Config, new UserObtentionService(context)).Disconnect(username, token);
        }

        public void PurgeUsers(UserbaseDbContext context)
        {
            new ManagementService(context).PurgeUsers();
        }

        public void PurgeConnectionTokens(UserbaseDbContext context)
        {
            new ManagementService(context).PurgeConnectionTokens();
        }

        public void PurgeRecoveryTokens(UserbaseDbContext context)
        {
            new ManagementService(context).PurgeRecoveryTokens();
        }

        public bool Deactivate(UserbaseDbContext context, string username, Guid token)
        {
            return new UserManagingService(context, new UserObtentionService(context), new UserConnectionService(context, UserbaseSystem.Config, new UserObtentionService(context)), new ValidationService()).Deactivate(username, token);
        }

        public bool SendRecoveryToken(UserbaseDbContext context, string username, IEmailSender smtp)
        {
            return new UserRecoveryService(context, new UserObtentionService(context), new UserConnectionService(context, UserbaseSystem.Config, new UserObtentionService(context))).SendRecoveryToken(username, smtp);
        }

        public ConnectUserResponse ResetPassword(UserbaseDbContext context, string username, string recoveryToken, string newPassword)
        {
            return new UserRecoveryService(context, new UserObtentionService(context), new UserConnectionService(context, UserbaseSystem.Config, new UserObtentionService(context))).ResetPassword(username, recoveryToken, newPassword);
        }

        public UserSummaryResponse UserSummary(UserbaseDbContext context, string askingUser, Guid token, string requestedUser)
        {
            return new UserInformationService(context, new UserConnectionService(context, UserbaseSystem.Config, new UserObtentionService(context))).UserSummary(askingUser, token, requestedUser);
        }

        public ListUsersResponse ListUsers(UserbaseDbContext context, string user, Guid token)
        {
            return new UserInformationService(context, new UserConnectionService(context, UserbaseSystem.Config, new UserObtentionService(context))).ListAllUsers(user, token);
        }

        public TokenSuccessResponse AddUserToGroup(UserbaseDbContext context, AddUserToGroupRequest request)
        {
            return new UserGroupingService(context, new UserConnectionService(context, UserbaseSystem.Config, new UserObtentionService(context))).AddUserToGroup(request);
        }

        public TokenSuccessResponse ExcludeUserFromGroup(UserbaseDbContext context, string requestingUsername, Guid token, string userToExclude, int idGroup)
        {
            return new UserGroupingService(context, new UserConnectionService(context, UserbaseSystem.Config, new UserObtentionService(context))).ExcludeUserFromGroup(requestingUsername, token, userToExclude, idGroup);
        }
    }
}
