using System.Data.Entity;
using System.Linq;
using Com.Ericmas001.Userbase.Entities;

namespace Com.Ericmas001.Userbase.Test.Util
{
    class DummyUserbaseDbContext : IUserbaseDbContext
    {
        private static int nextId = 1;
        public IDbSet<UserSetting> UserSettings { get; set; } = new DummyDbSet<UserSetting>();
        public IDbSet<UserRelation> UserRelations { get; set; } = new DummyDbSet<UserRelation>();
        public IDbSet<UserRelationType> UserRelationTypes { get; set; } = new DummyDbSet<UserRelationType>();
        public IDbSet<UserGroup> UserGroups { get; set; } = new DummyDbSet<UserGroup>();
        public IDbSet<UserGroupType> UserGroupTypes { get; set; } = new DummyDbSet<UserGroupType>();
        public IDbSet<UserAuthentication> UserAuthentications { get; set; } = new DummyDbSet<UserAuthentication>();
        public IDbSet<UserProfile> UserProfiles { get; set; } = new DummyDbSet<UserProfile>();
        public IDbSet<UserRecoveryToken> UserRecoveryTokens { get; set; } = new DummyDbSet<UserRecoveryToken>();
        public IDbSet<User> Users { get; set; } = new DummyDbSet<User>();
        public IDbSet<UserToken> UserTokens { get; set; } = new DummyDbSet<UserToken>();
        public int SaveChanges()
        {
            foreach (var user in Users)
            {
                if (user.Id == 0)
                    user.Id = nextId++;

                if (user.UserAuthentication != null)
                {
                    user.UserAuthentication.Id = user.Id;
                    if (!UserAuthentications.Contains(user.UserAuthentication))
                        UserAuthentications.Add(user.UserAuthentication);
                }
                if (user.UserSetting != null)
                {
                    user.UserSetting.Id = user.Id;
                    if (!UserSettings.Contains(user.UserSetting))
                        UserSettings.Add(user.UserSetting);
                }
                if (user.UserProfile != null)
                {
                    user.UserProfile.Id = user.Id;
                    if (!UserProfiles.Contains(user.UserProfile))
                        UserProfiles.Add(user.UserProfile);
                }

                foreach (var token in user.UserTokens)
                {
                    token.IdUser = user.Id;
                    if (!UserTokens.Contains(token))
                        UserTokens.Add(token);
                }
                foreach (var token in user.UserRecoveryTokens)
                {
                    token.IdUser = user.Id;
                    if (!UserRecoveryTokens.Contains(token))
                        UserRecoveryTokens.Add(token);
                }
                foreach (var grp in user.UserGroups)
                {
                    grp.IdUser = user.Id;
                    if (!UserGroups.Contains(grp))
                        UserGroups.Add(grp);
                }
            }
            foreach (var group in UserGroups)
            {
                if (group.Id == 0)
                    group.Id = nextId++;

                group.IdUserGroupType = group.UserGroupType.IdUserGroupType;
            }

            return 0;
        }
    }
}
