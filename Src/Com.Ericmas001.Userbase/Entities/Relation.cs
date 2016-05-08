using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// ReSharper disable All

namespace Com.Ericmas001.Userbase.Entities
{
    [Table("RelationsOfUser")]
    public class Relation : IEntityWithId
    {
        public int Id
        {
            get { return IdRelation; }
            set { IdRelation = value; }
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRelation { get; set; }

        public int IdUserOwner { get; set; }

        public int IdUserLinked { get; set; }

        public int IdRelationType { get; set; }

        public virtual RelationType RelationType { get; set; }

        public virtual User UserOwner { get; set; }

        public virtual User UserLinked { get; set; }
    }
}
