using System;
using Com.Ericmas001.Userbase.Test.Util;
using NUnit.Framework;

namespace Com.Ericmas001.Userbase.Test
{
    [TestFixture]
    public class ValidateTokenTest
    {
        [Test]
        public void WithNoUserReturnsFalse()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator:() => Values.Context );

            // Act
            var result = UserbaseSystem.ValidateToken(Values.UsernameSpongeBob, Values.ValidToken.Token);

            // Assert
            Assert.IsFalse(result.Success);
        }

        [Test]
        public void WithInvalidUserReturnsFalse()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                model.Users.Add(Values.UserSpongeBob);
                return model;
            });

            // Act
            var result = UserbaseSystem.ValidateToken(Values.UsernameDora, Values.ValidToken.Token);

            // Assert
            Assert.IsFalse(result.Success);
        }
        [Test]
        public void WithValidUserNoTokensReturnsFalse()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                model.Users.Add(Values.UserSpongeBob);
                return model;
            });

            // Act
            var result = UserbaseSystem.ValidateToken(Values.UsernameSpongeBob, Values.ValidToken.Token);

            // Assert
            Assert.IsFalse(result.Success);
        }
        [Test]
        public void WithValidUserInvalidTokenReturnsFalse()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                var u = Values.UserSpongeBob;
                u.UserTokens.Add(Values.ValidToken);
                model.Users.Add(u);
                return model;
            });

            // Act
            var result = UserbaseSystem.ValidateToken(Values.UsernameSpongeBob, Guid.NewGuid());

            // Assert
            Assert.IsFalse(result.Success);
        }
        [Test]
        public void WithValidUserExpiredTokenReturnsFalse()
        {
            // Arrange
            var tok = Values.ExpiredToken;
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                var u = Values.UserSpongeBob;
                u.UserTokens.Add(tok);
                model.Users.Add(u);
                return model;
            });

            // Act
            var result = UserbaseSystem.ValidateToken(Values.UsernameSpongeBob, tok.Token);

            // Assert
            Assert.IsFalse(result.Success);
        }
        [Test]
        public void WithValidUserValidTokenReturnsTrue()
        {
            // Arrange
            var tok = Values.ValidToken;
            var expiration = tok.Expiration;
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                var u = Values.UserSpongeBob;
                u.UserTokens.Add(tok);
                model.Users.Add(u);
                return model;
            });

            // Act
            var result = UserbaseSystem.ValidateToken(Values.UsernameSpongeBob, tok.Token);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(tok.Token, result.Token.Id);
            Assert.IsTrue(expiration < result.Token.ValidUntil);
        }
    }
}
