using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Com.Ericmas001.Userbase.Entities
{
    [Table("UserProfiles")]
    public class UserProfile
    {
        [Key]
        [ForeignKey("User")]
        public int IdUser { get; set; }

        [Required]
        [StringLength(100)]
        public string DisplayName { get; set; }

        public virtual User User { get; set; }
    }
}
