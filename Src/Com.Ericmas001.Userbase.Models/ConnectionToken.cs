using System;

namespace Com.Ericmas001.Userbase.Models
{
    public class ConnectionToken : Token<Guid>
    {
        public static DateTime NextExpiration => new ConnectionToken().CalculateNextExpiration();
        protected override DateTime CalculateNextExpiration() => DateTime.Now.AddMinutes(10);

        public ConnectionToken(Guid id, DateTime validUntil) : base(id, validUntil)
        {
        }

        public ConnectionToken()
        {
        }

        protected override Guid GenerateNewToken()
        {
            return Guid.NewGuid();
        }
    }
}