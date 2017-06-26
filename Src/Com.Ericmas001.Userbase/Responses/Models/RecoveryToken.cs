using System;
using System.Diagnostics.CodeAnalysis;

namespace Com.Ericmas001.Userbase.Responses.Models
{
    public class RecoveryToken : Token<string>
    {
        protected override DateTime CalculateNextExpiration() => DateTime.Now.AddDays(1);

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public RecoveryToken(string id, DateTime validUntil) : base(id, validUntil)
        {
        }

        public RecoveryToken()
        {
        }

        protected override string GenerateNewToken()
        {
            return Guid.NewGuid().ToString().ToUpper().Remove(8);
        }
    }
}
