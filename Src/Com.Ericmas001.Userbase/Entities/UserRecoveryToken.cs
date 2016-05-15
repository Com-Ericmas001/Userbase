using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// ReSharper disable All

namespace Com.Ericmas001.Userbase.Entities
{
    [Table("UserRecoveryTokens")]
    public class UserRecoveryToken : IEntityWithId
    {
        [NotMapped]
        public int Id
        {
            get { return IdUserRecoveryToken; }
            set { IdUserRecoveryToken = value; }
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUserRecoveryToken { get; set; }

        public int IdUser { get; set; }

        public Guid Token { get; set; }

        public DateTime Expiration { get; set; }

        public virtual User User { get; set; }
    }
}
