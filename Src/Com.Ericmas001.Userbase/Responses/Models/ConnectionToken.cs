using System;

namespace Com.Ericmas001.Userbase.Responses.Models
{
    public class ConnectionToken : Token
    {
        public static DateTime NextExpiration => new ConnectionToken().CalculateNextExpiration();
        protected override DateTime CalculateNextExpiration() => DateTime.Now.AddMinutes(10);

        public ConnectionToken(Guid id, DateTime validUntil) : base(id, validUntil)
        {
        }

        public ConnectionToken()
        {
        }
    }
}