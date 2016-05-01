using Com.Ericmas001.Userbase.Entities;
using System.Data.Entity;

namespace Com.Ericmas001.Userbase
{
    public class UserbaseDbContext : DbContext
    {
        public UserbaseDbContext()
            : base("name=UserbaseDbContext")
        {
        }
        public UserbaseDbContext(string connString)
            : base(connString)
        {
        }

        public virtual DbSet<Relation> Relations { get; set; }
        public virtual DbSet<RelationType> RelationTypes { get; set; }
        public virtual DbSet<UserAuthentication> UserAuthentications { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<UserRecoveryToken> UserRecoveryTokens { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserToken> UserTokens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RelationType>()
                .HasMany(e => e.Relations)
                .WithRequired(e => e.RelationType)
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

            modelBuilder.Entity<UserAuthentication>()
                .HasRequired(e => e.User)
                .WithOptional(e => e.UserAuthentication);

            modelBuilder.Entity<UserProfile>()
                .HasRequired(e => e.User)
                .WithOptional(e => e.UserProfile);
        }
    }
}
