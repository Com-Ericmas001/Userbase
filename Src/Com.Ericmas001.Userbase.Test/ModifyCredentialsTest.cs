using System;
using Com.Ericmas001.Userbase.Models.Requests;
using Com.Ericmas001.Userbase.Test.Util;
using Xunit;

namespace Com.Ericmas001.Userbase.Test
{
    [Collection("Com.Ericmas001.Userbase.Test")]
    public class ModifyCredentialsTest
    {
        [Fact]
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
            Assert.Null(result.Token);
            Assert.False(result.Success);
        }
        [Fact]
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
            Assert.Null(result.Token);
            Assert.False(result.Success);
        }
        [Fact]
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
            Assert.Null(result.Token);
            Assert.False(result.Success);
        }
        [Fact]
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
            Assert.Null(result.Token);
            Assert.False(result.Success);
        }
        [Fact]
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
            Assert.Null(result.Token);
            Assert.False(result.Success);
        }
        [Fact]
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
            Assert.Null(result.Token);
            Assert.False(result.Success);
        }
        [Fact]
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
            Assert.Null(result.Token);
            Assert.False(result.Success);
        }
        [Fact]
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
            Assert.NotNull(result.Token);
            Assert.True(tok.Expiration > originalTime);
            Assert.Equal(Values.EmailSpongeBobNewOne, u.UserAuthentication.RecoveryEmail);
            Assert.True(Values.VerifyPassword(Values.PasswordSpongeBobNewOne, u));
        }
        [Fact]
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
            Assert.NotNull(result.Token);
            Assert.True(tok.Expiration > originalTime);
            Assert.Equal(Values.EmailSpongeBobNewOne, u.UserAuthentication.RecoveryEmail);
            Assert.True(Values.VerifyPassword(Values.PasswordSpongeBob, u));
        }
        [Fact]
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
            Assert.NotNull(result.Token);
            Assert.True(tok.Expiration > originalTime);
            Assert.Equal(Values.EmailSpongeBob, u.UserAuthentication.RecoveryEmail);
            Assert.True(Values.VerifyPassword(Values.PasswordSpongeBobNewOne, u));
        }
    }
}
