using System;
using System.Linq;
using Com.Ericmas001.Userbase.Entities;
using Com.Ericmas001.Userbase.Requests;
using Com.Ericmas001.Userbase.Responses;

namespace Com.Ericmas001.Userbase.DbTasks
{
    public class UserGroupDbTask : AbstractDbTask
    {
        public UserGroupDbTask(UserbaseDbContext context) : base(context)
        {

        }
        public TokenSuccessResponse AddUserToGroup(AddUserToGroupRequest request)
        {
            var connection = UserbaseSystem.ValidateToken(request.Username, request.Token, Context);

            if (!connection.Success)
                return InvalidResponse;

            User requestingUser = Context.Users.Single(x => x.IdUser == connection.IdUser);

            if (requestingUser.UserGroups.All(x => x.UserGroupType.Name != UserGroupType.ADMIN_GRP))
                return InvalidResponse;

            User userToAdd = Context.Users.SingleOrDefault(x => x.Name == request.UserToAdd && x.Active);

            if (userToAdd == null || userToAdd.UserGroups.Any(x => x.IdUserGroupType == request.IdGroup))
                return InvalidResponse;

            userToAdd.UserGroups.Add(new UserGroup { IdUser = userToAdd.IdUser, IdUserGroupType = request.IdGroup });

            Context.SaveChanges();

            return connection;
        }
        public TokenSuccessResponse ExcludeUserFromGroup(string requestingUsername, Guid token, string userToExclude, int idGroup)
        {
            var connection = UserbaseSystem.ValidateToken(requestingUsername, token, Context);

            if (!connection.Success)
                return InvalidResponse;

            User requestingUser = Context.Users.Single(x => x.IdUser == connection.IdUser);

            if (requestingUser.UserGroups.All(x => x.UserGroupType.Name != UserGroupType.ADMIN_GRP))
                return InvalidResponse;

            User userToAdd = Context.Users.SingleOrDefault(x => x.Name == userToExclude && x.Active);

            if (userToAdd == null || userToAdd.UserGroups.All(x => x.IdUserGroupType != idGroup))
                return InvalidResponse;

            userToAdd.UserGroups.Remove(userToAdd.UserGroups.Single(x => x.IdUserGroupType == idGroup));

            Context.SaveChanges();

            return connection;
        }

        private TokenSuccessResponse InvalidResponse => new TokenSuccessResponse
        {
            Token = null,
            Success = false
        };
    }
}
