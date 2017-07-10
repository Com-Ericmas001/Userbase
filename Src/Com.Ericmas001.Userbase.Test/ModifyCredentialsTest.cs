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
            var util = new UserbaseSystemUtil(delegate{});

            // Act
            var result = util.System.ModifyCredentials(new ModifyCredentialsRequest
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
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                model.Users.Add(Values.UserSpongeBob);
            });

            // Act
            var result = util.System.ModifyCredentials(new ModifyCredentialsRequest
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
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                var u = Values.UserSpongeBob;
                u.UserTokens.Add(Values.ValidToken);
                model.Users.Add(u);
            });

            // Act
            var result = util.System.ModifyCredentials(new ModifyCredentialsRequest
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
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                var u = Values.UserSpongeBob;
                u.UserTokens.Add(tok);
                model.Users.Add(u);
            });

            // Act
            var result = util.System.ModifyCredentials(new ModifyCredentialsRequest
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
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                u.UserTokens.Add(tok);
                model.Users.Add(u);
            });

            var creds = Values.NewCredentialsSpongeBob;
            creds.Password = Values.PasswordInvalidChar;

            // Act
            var result = util.System.ModifyCredentials(new ModifyCredentialsRequest
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
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                u.UserTokens.Add(tok);
                model.Users.Add(u);
            });

            var creds = Values.NewCredentialsSpongeBob;
            creds.Email = Values.EmailNoArobas;

            // Act
            var result = util.System.ModifyCredentials(new ModifyCredentialsRequest
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
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                u.UserTokens.Add(tok);
                model.Users.Add(u);
                model.Users.Add(Values.UserDora);
            });

            var creds = Values.NewCredentialsSpongeBob;
            creds.Email = Values.EmailDora;

            // Act
            var result = util.System.ModifyCredentials(new ModifyCredentialsRequest
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
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                u.UserTokens.Add(tok);
                model.Users.Add(u);
                model.Users.Add(Values.UserDora);
            });

            // Act
            var result = util.System.ModifyCredentials(new ModifyCredentialsRequest
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
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                u.UserTokens.Add(tok);
                model.Users.Add(u);
                model.Users.Add(Values.UserDora);
            });

            var creds = Values.NewCredentialsSpongeBob;
            creds.Password = String.Empty;

            // Act
            var result = util.System.ModifyCredentials(new ModifyCredentialsRequest
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
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                u.UserTokens.Add(tok);
                model.Users.Add(u);
                model.Users.Add(Values.UserDora);
            });

            var creds = Values.NewCredentialsSpongeBob;
            creds.Email = String.Empty;

            // Act
            var result = util.System.ModifyCredentials(new ModifyCredentialsRequest
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
