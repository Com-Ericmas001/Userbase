using System;

namespace Com.Ericmas001.Userbase.Responses.Models
{
    public abstract class Token
    {
        public abstract DateTime CalculateNextExpiration();
        public Guid Id { get; set; }

        public DateTime ValidUntil { get; set; }

        public Token(Guid id, DateTime validUntil)
        {
            Id = id;
            ValidUntil = validUntil;
        }

        public Token()
        {
            Id = Guid.NewGuid();
            ValidUntil = CalculateNextExpiration();
        }
    }
}
