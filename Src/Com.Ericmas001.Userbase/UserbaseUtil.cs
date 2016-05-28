using System;
using System.Linq;
using Com.Ericmas001.Userbase.Entities;
using Com.Ericmas001.Userbase.Responses.Models;
using Com.Ericmas001.Userbase.Util;

namespace Com.Ericmas001.Userbase
{
    internal static class UserbaseUtil
    {
        public static string EncryptPassword(string password)
        {
            return BCrypt.HashPassword(UserbaseSystem.SaltPassword(password), BCrypt.GenerateSalt());
        }
        public static ConnectionToken ConnectionTokenIsValid(UserbaseDbContext context, int idUser, Guid token)
        {
            UserToken ut = GetConnectionTokenFromId(context, idUser, token);
            if (ut == null)
                return null;
            ut.Expiration = ConnectionToken.NextExpiration;
            context.SaveChanges();
            return new ConnectionToken(ut.Token, ut.Expiration);
        }

        public static UserToken GetConnectionTokenFromId(UserbaseDbContext context, int idUser, Guid token)
        {
            return context.UserTokens.AsEnumerable().SingleOrDefault(t => t.IdUser == idUser && t.Token == token && t.Expiration > DateTime.Now);
        }

        public static UserRecoveryToken GetRecoveryTokenFromId(UserbaseDbContext context, int idUser, Guid token)
        {
            return context.UserRecoveryTokens.AsEnumerable().SingleOrDefault(t => t.IdUser == idUser && t.Token == token && t.Expiration > DateTime.Now);
        }

        public static Token CreateConnectionToken(UserbaseDbContext context, int idUser)
        {
            var token = new ConnectionToken();
            context.UserTokens.Add(new UserToken { Token = token.Id, Expiration = token.ValidUntil, IdUser = idUser });
            context.SaveChanges();
            return token;
        }
    }
}
