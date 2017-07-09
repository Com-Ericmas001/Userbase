using System;
using System.Collections.Generic;
using System.Linq;
using Com.Ericmas001.Userbase.Models;
using Com.Ericmas001.Userbase.Models.Responses;

namespace Com.Ericmas001.Userbase.DbTasks
{
    public class ListUsersDbTask : AbstractDbTask
    {
        public ListUsersDbTask(UserbaseDbContext context) : base(context)
        {

        }
        public ListUsersResponse All(string username, Guid token)
        {
            var connection = UserbaseSystem.ValidateToken(username, token, Context);

            if (!connection.Success)
                return InvalidResponse;

            return new ListUsersResponse
            {
                Token = connection.Token,
                Success = true,
                Users = Context.Users.Where(x => x.Active).AsEnumerable().Select(x => new UserModel { IdUser = x.IdUser, Username = x.Name, DisplayName = x.UserProfile.DisplayName, Groups = x.UserGroups.ToDictionary(g => g.IdUserGroupType, g => g.UserGroupType.Name) })
            };
        }

        private ListUsersResponse InvalidResponse => new ListUsersResponse
        {
            Token = null,
            Success = false,
            Users = new UserModel[0]
        };
    }
}
