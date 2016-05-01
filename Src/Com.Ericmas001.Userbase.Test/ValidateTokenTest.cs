using System;
using Com.Ericmas001.Userbase.Entities;
using Com.Ericmas001.Userbase.Test.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Com.Ericmas001.Userbase.Test
{
    [TestClass]
    public class ValidateTokenTest
    {
        [TestMethod]
        public void WithNoUserReturnsFalse()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator:() => Values.Context );

            // Act
            var result = UserbaseSystem.ValidateToken(Values.UsernameSpongeBob, Values.ValidToken.Token);

            // Assert
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
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
        [TestMethod]
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
        [TestMethod]
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
        [TestMethod]
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
        [TestMethod]
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
