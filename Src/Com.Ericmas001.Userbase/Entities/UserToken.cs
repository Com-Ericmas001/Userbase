using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// ReSharper disable All

namespace Com.Ericmas001.Userbase.Entities
{
    [Table("UserTokens")]
    public class UserToken : IEntityWithId
    {
        public int Id
        {
            get { return IdUserToken; }
            set { IdUserToken = value; }
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUserToken { get; set; }

        public int IdUser { get; set; }

        public Guid Token { get; set; }

        public DateTime Expiration { get; set; }

        public virtual User User { get; set; }
    }
}
