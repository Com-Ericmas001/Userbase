using System;
using System.Diagnostics.CodeAnalysis;

namespace Com.Ericmas001.Userbase.Responses.Models
{
    public class RecoveryToken : Token
    {
        protected override DateTime CalculateNextExpiration() => DateTime.Now.AddDays(1);

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public RecoveryToken(Guid id, DateTime validUntil) : base(id, validUntil)
        {
        }

        public RecoveryToken()
        {
        }
    }
}
