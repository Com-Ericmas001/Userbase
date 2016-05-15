using System;
using Com.Ericmas001.Userbase.Test.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Com.Ericmas001.Userbase.Test
{
    [TestClass]
    public class UserSummaryTest
    {
        [TestMethod]
        public void WithInvalidUsernameReturnsFalse()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: () => Values.Context);

            // Act
            var result = UserbaseSystem.UserSummary(Values.UsernameSpongeBob, Guid.NewGuid(), Values.UsernameDora);

            // Assert
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [TestMethod]
        public void WithValidUsernameButNoTokenReturnsFalse()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                model.Users.Add(Values.UserSpongeBob);
                return model;
            });

            // Act
            var result = UserbaseSystem.UserSummary(Values.UsernameSpongeBob, Guid.NewGuid(), Values.UsernameDora);

            // Assert
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [TestMethod]
        public void WithValidUsernameButInvalidTokenReturnsFalse()
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
            var result = UserbaseSystem.UserSummary(Values.UsernameSpongeBob, Guid.NewGuid(), Values.UsernameDora);

            // Assert
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [TestMethod]
        public void WithValidUsernameButExpiredTokenReturnsFalse()
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
            var result = UserbaseSystem.UserSummary(Values.UsernameSpongeBob, tok.Token, Values.UsernameDora);

            // Assert
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [TestMethod]
        public void WithValidUsernameValidNotExpiredTokenButInvalidRequestedUsernameReturnsFalse()
        {
            // Arrange
            var tok = Values.ValidToken;
            var u = Values.UserSpongeBob;
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                u.UserTokens.Add(tok);
                model.Users.Add(u);
                return model;
            });

            // Act
            var result = UserbaseSystem.UserSummary(Values.UsernameSpongeBob, tok.Token, Values.UsernameDora);

            // Assert
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [TestMethod]
        public void WithValidUsernameValidNotExpiredToken()
        {
            // Arrange
            var tok = Values.ValidToken;
            var originalTime = tok.Expiration;
            var u = Values.UserSpongeBob;
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                u.UserTokens.Add(tok);
                model.Users.Add(u);
                model.Users.Add(Values.UserDora);
                return model;
            });

            // Act
            var result = UserbaseSystem.UserSummary(Values.UsernameSpongeBob, tok.Token, Values.UsernameDora);

            // Assert
            Assert.IsNotNull(result.Token);
            Assert.IsTrue(tok.Expiration > originalTime);
            Assert.AreEqual(Values.DisplayNameDora, result.DisplayName);
        }
    }
}
