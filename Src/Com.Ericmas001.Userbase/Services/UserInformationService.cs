using System;
using System.Collections.Generic;
using System.Linq;
using Com.Ericmas001.Userbase.Entities;
using Com.Ericmas001.Userbase.Models;
using Com.Ericmas001.Userbase.Models.Responses;
using Com.Ericmas001.Userbase.Models.ServiceInterfaces;

namespace Com.Ericmas001.Userbase.Services
{
    public class UserInformationService : IUserInformationService
    {
        private readonly IUserbaseDbContext m_DbContext;
        private readonly IUserConnectionService m_UserConnectionService;

        public UserInformationService(IUserbaseDbContext dbContext, IUserConnectionService userConnectionService)
        {
            m_DbContext = dbContext;
            m_UserConnectionService = userConnectionService;
        }

        public ListUsersResponse ListAllUsers(string askingUser, Guid token)
        {
            var connection = m_UserConnectionService.ConnectWithToken(askingUser, token);

            if (!connection.Success)
                return InvalidListResponse;

            return new ListUsersResponse
            {
                Token = connection.Token,
                Success = true,
                Users = m_DbContext.Users.Where(x => x.Active).AsEnumerable().Select(x => new UserModel
                {
                    IdUser = x.IdUser,
                    Username = x.Name,
                    DisplayName = x.UserProfile.DisplayName,
                    Groups = x.UserGroups.ToDictionary(g => g.IdUserGroupType, g => g.UserGroupType.Name)
                })
            };
        }

        public UserSummaryResponse UserSummary(string askingUser, Guid token, string requestedUser)
        {
            var connection = m_UserConnectionService.ConnectWithToken(askingUser, token);

            if (!connection.Success)
                return InvalidSummaryResponse;

            User u = m_DbContext.Users.SingleOrDefault(x => x.Name.Trim().ToLower() == requestedUser.Trim().ToLower());

            if (u == null)
                return InvalidSummaryResponse;

            return new UserSummaryResponse {DisplayName = u.UserProfile.DisplayName, Success = true, Token = connection.Token, Groups = u.UserGroups.Select(x => new GroupInfo {Id = x.IdUserGroupType, Name = x.UserGroupType.Name}).ToArray()};
        }

        private ListUsersResponse InvalidListResponse => new ListUsersResponse
        {
            Token = null,
            Success = false,
            Users = new UserModel[0]
        };

        private UserSummaryResponse InvalidSummaryResponse => new UserSummaryResponse
        {
            Token = null,
            Success = false,
            DisplayName = null,
            Groups = new GroupInfo[0]
        };
    }
}
