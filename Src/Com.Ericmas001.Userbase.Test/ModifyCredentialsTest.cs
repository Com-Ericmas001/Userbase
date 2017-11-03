using System;
using Com.Ericmas001.Userbase.Models.Requests;
using Com.Ericmas001.Userbase.Models.ServiceInterfaces;
using Com.Ericmas001.Userbase.Test.Util;
using FluentAssertions;
using Unity;
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
            var result = util.Container.Resolve<IUserManagingService>().ModifyCredentials(new ModifyCredentialsRequest
            {
                Username = Values.UsernameSpongeBob,
                Token = Guid.NewGuid(),
                Authentication = Values.NewCredentialsSpongeBob
            });

            // Assert
            result.Token.Should().BeNull();
            result.Success.Should().BeFalse();
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
            var result = util.Container.Resolve<IUserManagingService>().ModifyCredentials(new ModifyCredentialsRequest
            {
                Username = Values.UsernameSpongeBob,
                Token = Guid.NewGuid(),
                Authentication = Values.NewCredentialsSpongeBob
            });

            // Assert
            result.Token.Should().BeNull();
            result.Success.Should().BeFalse();
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
            var result = util.Container.Resolve<IUserManagingService>().ModifyCredentials(new ModifyCredentialsRequest
            {
                Username = Values.UsernameSpongeBob,
                Token = Guid.NewGuid(),
                Authentication = Values.NewCredentialsSpongeBob
            });

            // Assert
            result.Token.Should().BeNull();
            result.Success.Should().BeFalse();
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
            var result = util.Container.Resolve<IUserManagingService>().ModifyCredentials(new ModifyCredentialsRequest
            {
                Username = Values.UsernameSpongeBob,
                Token = tok.Token,
                Authentication = Values.NewCredentialsSpongeBob
            });

            // Assert
            result.Token.Should().BeNull();
            result.Success.Should().BeFalse();
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
            var result = util.Container.Resolve<IUserManagingService>().ModifyCredentials(new ModifyCredentialsRequest
            {
                Username = Values.UsernameSpongeBob,
                Token = tok.Token,
                Authentication = creds
            });

            // Assert
            result.Token.Should().BeNull();
            result.Success.Should().BeFalse();
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
            var result = util.Container.Resolve<IUserManagingService>().ModifyCredentials(new ModifyCredentialsRequest
            {
                Username = Values.UsernameSpongeBob,
                Token = tok.Token,
                Authentication = creds
            });

            // Assert
            result.Token.Should().BeNull();
            result.Success.Should().BeFalse();
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
            var result = util.Container.Resolve<IUserManagingService>().ModifyCredentials(new ModifyCredentialsRequest
            {
                Username = Values.UsernameSpongeBob,
                Token = tok.Token,
                Authentication = creds
            });

            // Assert
            result.Token.Should().BeNull();
            result.Success.Should().BeFalse();
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
            var result = util.Container.Resolve<IUserManagingService>().ModifyCredentials(new ModifyCredentialsRequest
            {
                Username = Values.UsernameSpongeBob,
                Token = tok.Token,
                Authentication = Values.NewCredentialsSpongeBob
            });

            // Assert
            result.Token.Should().NotBeNull();
            tok.Expiration.Should().BeAfter(originalTime);
            u.UserAuthentication.RecoveryEmail.Should().Be(Values.EmailSpongeBobNewOne);
            Values.VerifyPassword(Values.PasswordSpongeBobNewOne, u).Should().BeTrue();
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
            var result = util.Container.Resolve<IUserManagingService>().ModifyCredentials(new ModifyCredentialsRequest
            {
                Username = Values.UsernameSpongeBob,
                Token = tok.Token,
                Authentication = creds
            });

            // Assert
            result.Token.Should().NotBeNull();
            tok.Expiration.Should().BeAfter(originalTime);
            u.UserAuthentication.RecoveryEmail.Should().Be(Values.EmailSpongeBobNewOne);
            Values.VerifyPassword(Values.PasswordSpongeBob, u).Should().BeTrue();
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
            var result = util.Container.Resolve<IUserManagingService>().ModifyCredentials(new ModifyCredentialsRequest
            {
                Username = Values.UsernameSpongeBob,
                Token = tok.Token,
                Authentication = creds
            });

            // Assert
            result.Token.Should().NotBeNull();
            tok.Expiration.Should().BeAfter(originalTime);
            u.UserAuthentication.RecoveryEmail.Should().Be(Values.EmailSpongeBob);
            Values.VerifyPassword(Values.PasswordSpongeBobNewOne, u).Should().BeTrue();
        }
    }
}
