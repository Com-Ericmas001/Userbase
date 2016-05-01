using System;

namespace Com.Ericmas001.Userbase.Responses.Models
{
    public class RecoveryToken : Token
    {
        public override DateTime CalculateNextExpiration() => DateTime.Now.AddDays(1);

        public RecoveryToken(Guid id, DateTime validUntil) : base(id, validUntil)
        {
        }

        public RecoveryToken()
        {
        }
    }
}
