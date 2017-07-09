using Com.Ericmas001.Userbase.Entities;
using System.Data.Entity;
// ReSharper disable All

namespace Com.Ericmas001.Userbase
{
    public interface IUserbaseDbContext
    {
        IDbSet<UserSetting> UserSettings { get; set; }
        IDbSet<UserRelation> UserRelations { get; set; }
        IDbSet<UserRelationType> UserRelationTypes { get; set; }
        IDbSet<UserGroup> UserGroups { get; set; }
        IDbSet<UserGroupType> UserGroupTypes { get; set; }
        IDbSet<UserAuthentication> UserAuthentications { get; set; }
        IDbSet<UserProfile> UserProfiles { get; set; }
        IDbSet<UserRecoveryToken> UserRecoveryTokens { get; set; }
        IDbSet<User> Users { get; set; }
        IDbSet<UserToken> UserTokens { get; set; }
    }
}
