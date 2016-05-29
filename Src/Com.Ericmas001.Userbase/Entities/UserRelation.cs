using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// ReSharper disable All

namespace Com.Ericmas001.Userbase.Entities
{
    [Table("UserRelations")]
    public class UserRelation : IEntityWithId
    {
        [NotMapped]
        public int Id
        {
            get { return IdUserRelation; }
            set { IdUserRelation = value; }
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUserRelation { get; set; }

        public int IdUserOwner { get; set; }

        public int IdUserLinked { get; set; }

        public int IdUserRelationType { get; set; }

        public virtual UserRelationType UserRelationType { get; set; }

        public virtual User UserOwner { get; set; }

        public virtual User UserLinked { get; set; }
    }
}
