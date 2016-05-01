using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Ericmas001.Userbase.Entities;

namespace Com.Ericmas001.Userbase.Test.Util
{
    public static class UserbaseDbContextExtensions
    {
        public static void SetUpMocks(this UserbaseDbContext context)
        {
            var mockUsers = new MockDbSet<User>(new List<User>().AsQueryable());
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
    }
}
