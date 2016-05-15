using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;
// ReSharper disable All

namespace Com.Ericmas001.Userbase.Responses.Models
{
    [XmlInclude(typeof(ConnectionToken))]
    [XmlInclude(typeof(RecoveryToken))]
    public abstract class Token
    {
        protected abstract DateTime CalculateNextExpiration();
        public Guid Id { get; set; }

        public DateTime ValidUntil { get; set; }

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
