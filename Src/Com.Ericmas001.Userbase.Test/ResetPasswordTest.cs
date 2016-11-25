using System;
using Com.Ericmas001.Userbase.Test.Util;
using NUnit.Framework;

namespace Com.Ericmas001.Userbase.Test
{
    [TestFixture]
    public class ResetPasswordTest
    {
        [Test]
        public void EmptyUsersReturnsFalse()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: () => Values.Context);

            // Act
            var result = UserbaseSystem.ResetPassword(Values.UsernameSpongeBob, Guid.NewGuid(), Values.PasswordSpongeBobNewOne);

            // Assert
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [Test]
        public void ValidUsernameButNoTokenReturnsFalse()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                model.Users.Add(Values.UserSpongeBob);
                return model;
            });

            // Act
            var result = UserbaseSystem.ResetPassword(Values.UsernameSpongeBob, Guid.NewGuid(), Values.PasswordSpongeBobNewOne);

            // Assert
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [Test]
        public void ValidUsernameButInvalidTokenReturnsFalse()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                var u = Values.UserSpongeBob;
                u.UserRecoveryTokens.Add(Values.ValidRecoveryToken);
                model.Users.Add(u);
                return model;
            });

            // Act
            var result = UserbaseSystem.ResetPassword(Values.UsernameSpongeBob, Guid.NewGuid(), Values.PasswordSpongeBobNewOne);

            // Assert
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [Test]
        public void ValidUsernameButExpiredTokenReturnsFalse()
        {
            // Arrange
            var tok = Values.ExpiredRecoveryToken;
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                var u = Values.UserSpongeBob;
                u.UserRecoveryTokens.Add(tok);
                model.Users.Add(u);
                return model;
            });

            // Act
            var result = UserbaseSystem.ResetPassword(Values.UsernameSpongeBob, tok.Token, Values.PasswordSpongeBobNewOne);

            // Assert
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [Test]
        public void ValidUsernameValidNotExpiredTokenInvalidNewPasswordReturnsFalse()
        {
            // Arrange
            var tok = Values.ValidRecoveryToken;
            var u = Values.UserSpongeBob;
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                u.UserRecoveryTokens.Add(tok);
                model.Users.Add(u);
                return model;
            });

            // Act
            var result = UserbaseSystem.ResetPassword(Values.UsernameSpongeBob, tok.Token, Values.PasswordInvalidChar);

            // Assert
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [Test]
        public void ValidUsernameValidNotExpiredToken()
        {
            // Arrange
            var tok = Values.ValidRecoveryToken;
            var originalTime = tok.Expiration;
            var u = Values.UserSpongeBob;
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                u.UserRecoveryTokens.Add(tok);
                model.Users.Add(u);
                model.Users.Add(Values.UserDora);
                return model;
            });

            // Act
            var result = UserbaseSystem.ResetPassword(Values.UsernameSpongeBob, tok.Token, Values.PasswordSpongeBobNewOne);

            // Assert
            Assert.IsNotNull(result.Token);
            Assert.IsTrue(tok.Expiration < originalTime);
            Assert.IsTrue(Values.VerifyPassword(Values.PasswordSpongeBobNewOne, u));
        }
    }
}
