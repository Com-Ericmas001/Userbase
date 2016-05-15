using System.Collections.Generic;
using System.Linq;
using Com.Ericmas001.Userbase.Entities;

namespace Com.Ericmas001.Userbase.Test.Util
{
    public static class UserbaseDbContextExtensions
    {
        public static void SetUpMocks(this UserbaseDbContext context)
        {
            var mockUsers = new MockDbSet<User>(new List<User>().AsQueryable(), (i, user) => OnAddUser(user,context) );
            context.Users = mockUsers.Object;

            var mockUserTokens = new MockDbSet<UserToken>(new List<UserToken>().AsQueryable());
            context.UserTokens = mockUserTokens.Object;

            var mockUserRecoveryTokens = new MockDbSet<UserRecoveryToken>(new List<UserRecoveryToken>().AsQueryable());
            context.UserRecoveryTokens = mockUserRecoveryTokens.Object;

            var mockUserProfiles = new MockDbSet<UserProfile>(new List<UserProfile>().AsQueryable());
            context.UserProfiles = mockUserProfiles.Object;

            var mockUserAuthentications = new MockDbSet<UserAuthentication>(new List<UserAuthentication>().AsQueryable());
            context.UserAuthentications = mockUserAuthentications.Object;
        }

        private static void OnAddUser(User user, UserbaseDbContext context)
        {
            foreach (var userToken in user.UserTokens)
            {
                userToken.IdUser = user.IdUser;
                userToken.User = user;
                if (!context.UserTokens.Contains(userToken))
                    context.UserTokens.Add(userToken);
            }
            foreach (var userToken in user.UserRecoveryTokens)
            {
                userToken.IdUser = user.IdUser;
                userToken.User = user;
                if (!context.UserRecoveryTokens.Contains(userToken))
                    context.UserRecoveryTokens.Add(userToken);
            }
            if (user.UserAuthentication != null)
            {
                user.UserAuthentication.IdUser = user.IdUser;
                user.UserAuthentication.User = user;
                if (!context.UserAuthentications.Contains(user.UserAuthentication))
                    context.UserAuthentications.Add(user.UserAuthentication);
            }
        }
    }
}
