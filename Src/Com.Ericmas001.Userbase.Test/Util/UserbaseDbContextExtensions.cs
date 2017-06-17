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

            var mockUserGroups = new MockDbSet<UserGroup>(new List<UserGroup>().AsQueryable(), (i, grp) => OnAddUserGroup(grp, context));
            context.UserGroups = mockUserGroups.Object;

            var mockUserGroupTypes = new MockDbSet<UserGroupType>(new List<UserGroupType>().AsQueryable());
            context.UserGroupTypes = mockUserGroupTypes.Object;

            var mockUserRelations = new MockDbSet<UserRelation>(new List<UserRelation>().AsQueryable());
            context.UserRelations = mockUserRelations.Object;

            var mockUserSettings = new MockDbSet<UserSetting>(new List<UserSetting>().AsQueryable());
            context.UserSettings = mockUserSettings.Object;
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
            foreach (var userGroup in user.UserGroups)
            {
                userGroup.IdUser = user.IdUser;
                userGroup.User = user;
                if (!context.UserGroups.Contains(userGroup))
                    context.UserGroups.Add(userGroup);
            }
            foreach (var userRelation in user.RelationsOfUser)
            {
                userRelation.IdUserOwner = user.IdUser;
                userRelation.UserOwner = user;
                if (!context.UserRelations.Contains(userRelation))
                    context.UserRelations.Add(userRelation);
            }
            foreach (var userRelation in user.RelationsToThisUser)
            {
                userRelation.IdUserLinked = user.IdUser;
                userRelation.UserLinked = user;
                if (!context.UserRelations.Contains(userRelation))
                    context.UserRelations.Add(userRelation);
            }
            if (user.UserAuthentication != null)
            {
                user.UserAuthentication.IdUser = user.IdUser;
                user.UserAuthentication.User = user;
                if (!context.UserAuthentications.Contains(user.UserAuthentication))
                    context.UserAuthentications.Add(user.UserAuthentication);
            }
            if (user.UserProfile != null)
            {
                user.UserProfile.IdUser = user.IdUser;
                user.UserProfile.User = user;
                if (!context.UserProfiles.Contains(user.UserProfile))
                    context.UserProfiles.Add(user.UserProfile);
            }
            if (user.UserSetting != null)
            {
                user.UserSetting.IdUser = user.IdUser;
                user.UserSetting.User = user;
                if (!context.UserSettings.Contains(user.UserSetting))
                    context.UserSettings.Add(user.UserSetting);
            }
        }

        private static void OnAddUserGroup(UserGroup grp, UserbaseDbContext context)
        {
            if (grp.UserGroupType != null)
            {
                if(!grp.UserGroupType.UserGroups.Contains(grp))
                    grp.UserGroupType.UserGroups.Add(grp);

                if (!context.UserGroupTypes.Contains(grp.UserGroupType))
                    context.UserGroupTypes.Add(grp.UserGroupType);
            }
        }
    }
}
