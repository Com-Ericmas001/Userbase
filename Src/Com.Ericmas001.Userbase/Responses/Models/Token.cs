using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;
// ReSharper disable All

namespace Com.Ericmas001.Userbase.Responses.Models
{
    [XmlInclude(typeof(ConnectionToken))]
    [XmlInclude(typeof(RecoveryToken))]
    public abstract class Token<TToken>
    {
        protected abstract DateTime CalculateNextExpiration();
        public TToken Id { get; set; }

        public DateTime ValidUntil { get; set; }

        protected Token(TToken id, DateTime validUntil)
        {
            Id = id;
            ValidUntil = validUntil;
        }

        [SuppressMessage("ReSharper", "VirtualMemberCallInContructor")]
        protected Token()
        {
            Id = GenerateNewToken();
            ValidUntil = CalculateNextExpiration();
        }

        protected abstract TToken GenerateNewToken();
    }
}
