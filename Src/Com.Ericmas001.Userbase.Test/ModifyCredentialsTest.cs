using System;
using Com.Ericmas001.Userbase.Requests;
using Com.Ericmas001.Userbase.Test.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Com.Ericmas001.Userbase.Test
{
    [TestClass]
    public class ModifyCredentialsTest
    {
        [TestMethod]
        public void EmptyUsersReturnsFalse()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: () => Values.Context);

            // Act
            var result = UserbaseSystem.ModifyCredentials(new ModifyCredentialsRequest
            {
                Username = Values.UsernameSpongeBob,
                Token = Guid.NewGuid(),
                Authentication = Values.NewCredentialsSpongeBob
            });

            // Assert
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [TestMethod]
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
            var result = UserbaseSystem.ModifyCredentials(new ModifyCredentialsRequest
            {
                Username = Values.UsernameSpongeBob,
                Token = Guid.NewGuid(),
                Authentication = Values.NewCredentialsSpongeBob
            });

            // Assert
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [TestMethod]
        public void ValidUsernameButInvalidTokenReturnsFalse()
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
            var result = UserbaseSystem.ModifyCredentials(new ModifyCredentialsRequest
            {
                Username = Values.UsernameSpongeBob,
                Token = Guid.NewGuid(),
                Authentication = Values.NewCredentialsSpongeBob
            });

            // Assert
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [TestMethod]
        public void ValidUsernameButExpiredTokenReturnsFalse()
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
            var result = UserbaseSystem.ModifyCredentials(new ModifyCredentialsRequest
            {
                Username = Values.UsernameSpongeBob,
                Token = tok.Token,
                Authentication = Values.NewCredentialsSpongeBob
            });

            // Assert
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [TestMethod]
        public void ValidUsernameValidNotExpiredTokenInvalidNewPasswordReturnsFalse()
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

            var creds = Values.NewCredentialsSpongeBob;
            creds.Password = Values.PasswordInvalidChar;

            // Act
            var result = UserbaseSystem.ModifyCredentials(new ModifyCredentialsRequest
            {
                Username = Values.UsernameSpongeBob,
                Token = tok.Token,
                Authentication = creds
            });

            // Assert
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [TestMethod]
        public void ValidUsernameValidNotExpiredTokenInvalidNewEmailReturnsFalse()
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

            var creds = Values.NewCredentialsSpongeBob;
            creds.Email = Values.EmailNoArobas;

            // Act
            var result = UserbaseSystem.ModifyCredentials(new ModifyCredentialsRequest
            {
                Username = Values.UsernameSpongeBob,
                Token = tok.Token,
                Authentication = creds
            });

            // Assert
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [TestMethod]
        public void ValidUsernameValidNotExpiredTokenExistingNewEmailReturnsFalse()
        {
            // Arrange
            var tok = Values.ValidToken;
            var u = Values.UserSpongeBob;
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                u.UserTokens.Add(tok);
                model.Users.Add(u);
                model.Users.Add(Values.UserDora);
                return model;
            });

            var creds = Values.NewCredentialsSpongeBob;
            creds.Email = Values.EmailDora;

            // Act
            var result = UserbaseSystem.ModifyCredentials(new ModifyCredentialsRequest
            {
                Username = Values.UsernameSpongeBob,
                Token = tok.Token,
                Authentication = creds
            });

            // Assert
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [TestMethod]
        public void ValidUsernameValidNotExpiredToken()
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
            var result = UserbaseSystem.ModifyCredentials(new ModifyCredentialsRequest
            {
                Username = Values.UsernameSpongeBob,
                Token = tok.Token,
                Authentication = Values.NewCredentialsSpongeBob
            });

            // Assert
            Assert.IsNotNull(result.Token);
            Assert.IsTrue(tok.Expiration > originalTime);
            Assert.AreEqual(Values.EmailSpongeBobNewOne, u.UserAuthentication.RecoveryEmail);
            Assert.IsTrue(Values.VerifyPassword(Values.PasswordSpongeBobNewOne, u));
        }
        [TestMethod]
        public void ValidUsernameValidNotExpiredTokenNotChangingPassword()
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

            var creds = Values.NewCredentialsSpongeBob;
            creds.Password = String.Empty;

            // Act
            var result = UserbaseSystem.ModifyCredentials(new ModifyCredentialsRequest
            {
                Username = Values.UsernameSpongeBob,
                Token = tok.Token,
                Authentication = creds
            });

            // Assert
            Assert.IsNotNull(result.Token);
            Assert.IsTrue(tok.Expiration > originalTime);
            Assert.AreEqual(Values.EmailSpongeBobNewOne, u.UserAuthentication.RecoveryEmail);
            Assert.IsTrue(Values.VerifyPassword(Values.PasswordSpongeBob, u));
        }
        [TestMethod]
        public void ValidUsernameValidNotExpiredTokenNotChangingEmail()
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

            var creds = Values.NewCredentialsSpongeBob;
            creds.Email = String.Empty;

            // Act
            var result = UserbaseSystem.ModifyCredentials(new ModifyCredentialsRequest
            {
                Username = Values.UsernameSpongeBob,
                Token = tok.Token,
                Authentication = creds
            });

            // Assert
            Assert.IsNotNull(result.Token);
            Assert.IsTrue(tok.Expiration > originalTime);
            Assert.AreEqual(Values.EmailSpongeBob, u.UserAuthentication.RecoveryEmail);
            Assert.IsTrue(Values.VerifyPassword(Values.PasswordSpongeBobNewOne, u));
        }
    }
}
