using System;
using System.Collections.Generic;
using System.Linq;
using Com.Ericmas001.Userbase.Entities;
using Com.Ericmas001.Userbase.Models;
using Com.Ericmas001.Userbase.Models.Requests;
using Com.Ericmas001.Userbase.Models.Responses;
using Com.Ericmas001.Userbase.Services.Interfaces;

namespace Com.Ericmas001.Userbase.Services
{
    public class UserGroupingService : IUserGroupingService
    {
        private readonly IUserbaseDbContext m_DbContext;
        private readonly IUserConnectionService m_UserConnectionService;

        public UserGroupingService(IUserbaseDbContext dbContext, IUserConnectionService userConnectionService)
        {
            m_DbContext = dbContext;
            m_UserConnectionService = userConnectionService;
        }

        public TokenSuccessResponse AddUserToGroup(AddUserToGroupRequest request)
        {
            var connection = m_UserConnectionService.ConnectWithToken(request.Username, request.Token);

            if (!connection.Success)
                return InvalidResponse;

            User requestingUser = m_DbContext.Users.Single(x => x.IdUser == connection.IdUser);

            if (requestingUser.UserGroups.All(x => x.UserGroupType.Name != UserGroupType.ADMIN_GRP))
                return InvalidResponse;

            User userToAdd = m_DbContext.Users.SingleOrDefault(x => x.Name == request.UserToAdd && x.Active);

            if (userToAdd == null || userToAdd.UserGroups.Any(x => x.IdUserGroupType == request.IdGroup))
                return InvalidResponse;

            userToAdd.UserGroups.Add(new UserGroup { IdUser = userToAdd.IdUser, IdUserGroupType = request.IdGroup });

            m_DbContext.SaveChanges();

            return connection;
        }

        public TokenSuccessResponse ExcludeUserFromGroup(string requestingUsername, Guid token, string userToExclude, int idGroup)
        {
            var connection = m_UserConnectionService.ConnectWithToken(requestingUsername, token);

            if (!connection.Success)
                return InvalidResponse;

            User requestingUser = m_DbContext.Users.Single(x => x.IdUser == connection.IdUser);

            if (requestingUser.UserGroups.All(x => x.UserGroupType.Name != UserGroupType.ADMIN_GRP))
                return InvalidResponse;

            User userToAdd = m_DbContext.Users.SingleOrDefault(x => x.Name == userToExclude && x.Active);

            if (userToAdd == null || userToAdd.UserGroups.All(x => x.IdUserGroupType != idGroup))
                return InvalidResponse;

            userToAdd.UserGroups.Remove(userToAdd.UserGroups.Single(x => x.IdUserGroupType == idGroup));

            m_DbContext.SaveChanges();

            return connection;
        }

        private TokenSuccessResponse InvalidResponse => new TokenSuccessResponse
        {
            Token = null,
            Success = false
        };
    }
}
