using System;
using System.Diagnostics.CodeAnalysis;

namespace Com.Ericmas001.Userbase.Responses.Models
{
    public abstract class Token
    {
        protected abstract DateTime CalculateNextExpiration();
        public Guid Id { get; }

        public DateTime ValidUntil { get; }

        protected Token(Guid id, DateTime validUntil)
        {
            Id = id;
            ValidUntil = validUntil;
        }

        [SuppressMessage("ReSharper", "VirtualMemberCallInContructor")]
        protected Token()
        {
            Id = Guid.NewGuid();
            ValidUntil = CalculateNextExpiration();
        }
    }
}
