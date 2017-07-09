using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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

        [StringLength(100)]
        public string Token { get; set; }

        public DateTime Expiration { get; set; }

        public virtual User User { get; set; }





        public static UserRecoveryToken FromId(IUserbaseDbContext context, int idUser, string token)
        {
            return context.UserRecoveryTokens.AsEnumerable().SingleOrDefault(t => t.IdUser == idUser && t.Token == token && t.Expiration > DateTime.Now);
        }
    }
}
