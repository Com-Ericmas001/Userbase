using Com.Ericmas001.Userbase.Entities;
using System.Data.Entity;
// ReSharper disable All

namespace Com.Ericmas001.Userbase
{
    public class UserbaseDbContext : DbContext, IUserbaseDbContext
    {
        public UserbaseDbContext(string connString = "name=UserbaseDbContext")
            : base(connString)
        {
        }

        public virtual IDbSet<UserSetting> UserSettings { get; set; }
        public virtual IDbSet<UserRelation> UserRelations { get; set; }
        public virtual IDbSet<UserRelationType> UserRelationTypes { get; set; }
        public virtual IDbSet<UserGroup> UserGroups { get; set; }
        public virtual IDbSet<UserGroupType> UserGroupTypes { get; set; }
        public virtual IDbSet<UserAuthentication> UserAuthentications { get; set; }
        public virtual IDbSet<UserProfile> UserProfiles { get; set; }
        public virtual IDbSet<UserRecoveryToken> UserRecoveryTokens { get; set; }
        public virtual IDbSet<User> Users { get; set; }
        public virtual IDbSet<UserToken> UserTokens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccessType>()
                .HasMany(e => e.UserSettingListFriends)
                .WithRequired(e => e.UserAccessTypeListFriends)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserGroupType>()
                .HasMany(e => e.UserGroups)
                .WithRequired(e => e.UserGroupType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserRelationType>()
                .HasMany(e => e.UserRelations)
                .WithRequired(e => e.UserRelationType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserAccessType>()
                .HasMany(e => e.UserSettingListFriends)
                .WithRequired(e => e.UserAccessTypeListFriends)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserAuthentication>()
                .Property(e => e.Password)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .HasMany(e => e.RelationsOfUser)
                .WithRequired(e => e.UserOwner)
                .HasForeignKey(e => e.IdUserOwner)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.RelationsToThisUser)
                .WithRequired(e => e.UserLinked)
                .HasForeignKey(e => e.IdUserLinked)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserTokens)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserRecoveryTokens)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserGroups)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserAuthentication>()
                .HasRequired(e => e.User)
                .WithOptional(e => e.UserAuthentication);

            modelBuilder.Entity<UserProfile>()
                .HasRequired(e => e.User)
                .WithOptional(e => e.UserProfile);

            modelBuilder.Entity<UserSetting>()
                .HasRequired(e => e.User)
                .WithOptional(e => e.UserSetting);
        }
    }
}
