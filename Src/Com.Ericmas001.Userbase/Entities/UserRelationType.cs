using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// ReSharper disable All

namespace Com.Ericmas001.Userbase.Entities
{
    [Table("UserRelationTypes")]
    public class UserRelationType : IEntityWithId
    {
        [NotMapped]
        public int Id
        {
            get { return IdUserRelationType; }
            set { IdUserRelationType = value; }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserRelationType()
        {
            UserRelations = new HashSet<UserRelation>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUserRelationType { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRelation> UserRelations { get; set; }
    }
}
