using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Com.Ericmas001.Userbase.Entities
{
    [Table("Users")]
    public class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            RelationsOfUser = new HashSet<Relation>();
            RelationsToThisUser = new HashSet<Relation>();
            UserTokens = new HashSet<UserToken>();
            UserRecoveryTokens = new HashSet<UserRecoveryToken>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUser { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public bool Active { get; set; } = true;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Relation> RelationsOfUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Relation> RelationsToThisUser { get; set; }

        public virtual UserAuthentication UserAuthentication { get; set; }

        public virtual UserProfile UserProfile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserToken> UserTokens { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRecoveryToken> UserRecoveryTokens { get; set; }
    }
}
