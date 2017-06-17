using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// ReSharper disable All

namespace Com.Ericmas001.Userbase.Entities
{
    [Table("UserGroups")]
    public class UserGroup : IEntityWithId
    {
        [NotMapped]
        public int Id
        {
            get { return IdUserGroup; }
            set { IdUserGroup = value; }
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUserGroup { get; set; }

        public int IdUser { get; set; }

        public int IdUserGroupType { get; set; }

        public virtual UserGroupType UserGroupType { get; set; }

        public virtual User User { get; set; }
    }
}
