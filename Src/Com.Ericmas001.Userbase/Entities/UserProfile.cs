using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// ReSharper disable All

namespace Com.Ericmas001.Userbase.Entities
{
    [Table("UserProfiles")]
    public class UserProfile : IEntityWithId
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

        [Required]
        [StringLength(100)]
        public string DisplayName { get; set; }

        public virtual User User { get; set; }
    }
}
