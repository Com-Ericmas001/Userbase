using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// ReSharper disable All

namespace Com.Ericmas001.Userbase.Entities
{
    [Table("UserSettings")]
    public class UserSetting : IEntityWithId
    {
        [NotMapped]
        public int Id
        {
            get { return IdUser; }
            set { IdUser = value; }
        }

        [Key]
        [ForeignKey("User")]
        public int IdUser { get; set; }

        public int IdUserAccessTypeListFriends { get; set; }

        public virtual UserAccessType UserAccessTypeListFriends { get; set; }

        public virtual User User { get; set; }
    }
}
