using System;
using Com.Ericmas001.Userbase.Models;
using Com.Ericmas001.Userbase.Models.Requests;
using Com.Ericmas001.Userbase.Models.Responses;
using Com.Ericmas001.Userbase.Services;
using Com.Ericmas001.Userbase.Services.Interfaces;
using Microsoft.Practices.Unity;

namespace Com.Ericmas001.Userbase
{
    public class UserbaseSystem
    {
        private readonly IUnityContainer m_Container;

        public UserbaseSystem(IUnityContainer container = null, string salt = null)
        {
            m_Container = container ?? new UnityContainer();

            RegisterTypes(salt ?? string.Empty);
        }

        private void RegisterTypes(string salt)
        {
            m_Container.RegisterType<IUserbaseDbContext, UserbaseDbContext>();

            m_Container.RegisterType<IValidationService, ValidationService>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<ISecurityService, BCryptSecurityService>(new ContainerControlledLifetimeManager(), new InjectionConstructor(salt));

            m_Container.RegisterType<IManagementService, ManagementService>();
            m_Container.RegisterType<IUserConnectionService, UserConnectionService>();
            m_Container.RegisterType<IUserGroupingService, UserGroupingService>();
            m_Container.RegisterType<IUserInformationService, UserInformationService>();
            m_Container.RegisterType<IUserManagingService, UserManagingService>();
            m_Container.RegisterType<IUserObtentionService, UserObtentionService>();
            m_Container.RegisterType<IUserRecoveryService, UserRecoveryService>();
        }

        public int IdFromUsername(string username)
        {
            return m_Container.Resolve<IUserObtentionService>().FromUsername(username);
        }

        public bool UsernameExists(string username)
        {
            return m_Container.Resolve<IUserObtentionService>().UsernameExists(username);
        }

        public int IdFromEmail(string email)
        {
            return m_Container.Resolve<IUserObtentionService>().FromEmail(email);
        }

        public bool EmailExists(string email)
        {
            return m_Container.Resolve<IUserObtentionService>().EmailExists(email);
        }

        public ConnectUserResponse ValidateCredentials(string username, string password)
        {
            return m_Container.Resolve<IUserConnectionService>().ConnectWithPassword(username, password);
        }

        public ConnectUserResponse ValidateToken(string username, Guid token)
        {
            return m_Container.Resolve<IUserConnectionService>().ConnectWithToken(username, token);
        }

        public ConnectUserResponse CreateUser(CreateUserRequest request)
        {
            return m_Container.Resolve<IUserManagingService>().CreateUser(request);
        }

        public TokenSuccessResponse ModifyCredentials(ModifyCredentialsRequest request)
        {
            return m_Container.Resolve<IUserManagingService>().ModifyCredentials(request);
        }

        public TokenSuccessResponse ModifyProfile(ModifyProfileRequest request)
        {
            return m_Container.Resolve<IUserManagingService>().ModifyProfile(request);
        }

        public bool Disconnect(string username, Guid token)
        {
            return m_Container.Resolve<IUserConnectionService>().Disconnect(username, token);
        }

        public void PurgeUsers(UserbaseDbContext existingContext = null)
        {
            m_Container.Resolve<IManagementService>().PurgeUsers();
        }

        public void PurgeConnectionTokens(UserbaseDbContext existingContext = null)
        {
            m_Container.Resolve<IManagementService>().PurgeConnectionTokens();
        }

        public void PurgeRecoveryTokens(UserbaseDbContext existingContext = null)
        {
            m_Container.Resolve<IManagementService>().PurgeRecoveryTokens();
        }

        public bool Deactivate(string username, Guid token)
        {
            return m_Container.Resolve<IUserManagingService>().Deactivate(username, token);
        }

        public bool SendRecoveryToken(string username, IEmailSender smtp)
        {
            return m_Container.Resolve<IUserRecoveryService>().SendRecoveryToken(username, smtp);
        }

        public ConnectUserResponse ResetPassword(string username, string recoveryToken, string newPassword)
        {
            return m_Container.Resolve<IUserRecoveryService>().ResetPassword(username, recoveryToken, newPassword);
        }

        public UserSummaryResponse UserSummary(string askingUser, Guid token, string requestedUser)
        {
            return m_Container.Resolve<IUserInformationService>().UserSummary(askingUser, token, requestedUser);
        }

        public ListUsersResponse ListUsers(string username, Guid token)
        {
            return m_Container.Resolve<IUserInformationService>().ListAllUsers(username, token);
        }

        public TokenSuccessResponse AddUserToGroup(AddUserToGroupRequest request)
        {
            return m_Container.Resolve<IUserGroupingService>().AddUserToGroup(request);
        }

        public TokenSuccessResponse ExcludeUserFromGroup(string requestingUsername, Guid token, string userToExclude, int idGroup)
        {
            return m_Container.Resolve<IUserGroupingService>().ExcludeUserFromGroup(requestingUsername, token, userToExclude, idGroup);
        }
    }
}
