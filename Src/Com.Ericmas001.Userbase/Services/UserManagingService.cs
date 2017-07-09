using System;
using System.Linq;
using Com.Ericmas001.Userbase.Entities;
using Com.Ericmas001.Userbase.Models.Requests;
using Com.Ericmas001.Userbase.Models.Responses;
using Com.Ericmas001.Userbase.Services.Interfaces;

namespace Com.Ericmas001.Userbase.Services
{
    public class UserManagingService : IUserManagingService
    {
        private readonly IUserbaseDbContext m_DbContext;
        private readonly ISecurityService m_SecurityService;
        private readonly IUserObtentionService m_UserObtentionService;
        private readonly IUserConnectionService m_UserConnectionService;
        private readonly IValidationService m_ValidationService;

        public UserManagingService(IUserbaseDbContext dbContext, ISecurityService securityService, IUserObtentionService userObtentionService, IUserConnectionService userConnectionService, IValidationService validationService)
        {
            m_DbContext = dbContext;
            m_SecurityService = securityService;
            m_UserObtentionService = userObtentionService;
            m_UserConnectionService = userConnectionService;
            m_ValidationService = validationService;
        }

        public ConnectUserResponse CreateUser(CreateUserRequest request)
        {
            if (m_UserObtentionService.UsernameExists(request.Username))
                return InvalidResponse;

            if (m_UserObtentionService.EmailExists(request.Authentication.Email))
                return InvalidResponse;

            if (!m_ValidationService.ValidateUsername(request.Username))
                return InvalidResponse;

            if (!m_ValidationService.ValidateEmail(request.Authentication.Email))
                return InvalidResponse;

            if (!m_ValidationService.ValidatePassword(request.Authentication.Password))
                return InvalidResponse;

            if (!m_ValidationService.ValidateDisplayName(request.Profile.DisplayName))
                return InvalidResponse;


            User u = new User
            {
                Name = request.Username,
                UserProfile = new UserProfile
                {
                    DisplayName = request.Profile.DisplayName
                },
                UserAuthentication = new UserAuthentication
                {
                    Password = m_SecurityService.EncryptPassword(request.Authentication.Password),
                    RecoveryEmail = request.Authentication.Email
                }
            };

            m_DbContext.Users.Add(u);
            m_DbContext.SaveChanges();

            return m_UserConnectionService.ConnectWithPassword(request.Username, request.Authentication.Password);
        }

        public TokenSuccessResponse ModifyProfile(ModifyProfileRequest request)
        {

            var connection = m_UserConnectionService.ConnectWithToken(request.Username, request.Token);

            if (!connection.Success)
                return InvalidResponse;

            User u = m_DbContext.Users.Single(x => x.IdUser == connection.IdUser);

            if (!string.IsNullOrEmpty(request.Profile.DisplayName))
            {
                if (!m_ValidationService.ValidateDisplayName(request.Profile.DisplayName))
                    return InvalidResponse;
                u.UserProfile.DisplayName = request.Profile.DisplayName;
            }

            m_DbContext.SaveChanges();

            return connection;
        }

        public TokenSuccessResponse ModifyCredentials(ModifyCredentialsRequest request)
        {
            var connection = m_UserConnectionService.ConnectWithToken(request.Username, request.Token);

            if (!connection.Success)
                return InvalidResponse;

            User u = m_DbContext.Users.Single(x => x.IdUser == connection.IdUser);

            if (!string.IsNullOrEmpty(request.Authentication.Password))
            {
                if (!m_ValidationService.ValidatePassword(request.Authentication.Password))
                    return InvalidResponse;
                u.UserAuthentication.Password = m_SecurityService.EncryptPassword(request.Authentication.Password);
            }

            if (!string.IsNullOrEmpty(request.Authentication.Email))
            {
                if (!m_ValidationService.ValidateEmail(request.Authentication.Email))
                    return InvalidResponse;

                if (m_UserObtentionService.EmailExists(request.Authentication.Email))
                    return InvalidResponse;

                u.UserAuthentication.RecoveryEmail = request.Authentication.Email;
            }

            m_DbContext.SaveChanges();

            return connection;
        }

        public bool Deactivate(string username, Guid token)
        {
            var connection = m_UserConnectionService.ConnectWithToken(username, token);

            if (!connection.Success)
                return false;

            User u = m_DbContext.Users.Single(x => x.IdUser == connection.IdUser);
            u.Active = false;
            m_DbContext.SaveChanges();

            return true;
        }
        private ConnectUserResponse InvalidResponse => new ConnectUserResponse
        {
            Token = null,
            Success = false
        };
    }
}
