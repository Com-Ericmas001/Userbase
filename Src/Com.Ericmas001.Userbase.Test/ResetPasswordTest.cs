using System;
using Com.Ericmas001.Userbase.Models.ServiceInterfaces;
using Com.Ericmas001.Userbase.Test.Util;
using FluentAssertions;
using Unity;
using Xunit;

namespace Com.Ericmas001.Userbase.Test
{

    [Collection("Com.Ericmas001.Userbase.Test")]
    public class ResetPasswordTest
    {
        [Fact]
        public void EmptyUsersReturnsFalse()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate { });

            // Act
            var result = util.Container.Resolve<IUserRecoveryService>().ResetPassword(Values.UsernameSpongeBob, Guid.NewGuid().ToString(), Values.PasswordSpongeBobNewOne);

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
            var result = util.Container.Resolve<IUserRecoveryService>().ResetPassword(Values.UsernameSpongeBob, Guid.NewGuid().ToString(), Values.PasswordSpongeBobNewOne);

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
                u.UserRecoveryTokens.Add(Values.ValidRecoveryToken);
                model.Users.Add(u);
            });

            // Act
            var result = util.Container.Resolve<IUserRecoveryService>().ResetPassword(Values.UsernameSpongeBob, Guid.NewGuid().ToString(), Values.PasswordSpongeBobNewOne);

            // Assert
            result.Token.Should().BeNull();
            result.Success.Should().BeFalse();
        }
        [Fact]
        public void ValidUsernameButExpiredTokenReturnsFalse()
        {
            // Arrange
            var tok = Values.ExpiredRecoveryToken;
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                var u = Values.UserSpongeBob;
                u.UserRecoveryTokens.Add(tok);
                model.Users.Add(u);
            });

            // Act
            var result = util.Container.Resolve<IUserRecoveryService>().ResetPassword(Values.UsernameSpongeBob, tok.Token, Values.PasswordSpongeBobNewOne);

            // Assert
            result.Token.Should().BeNull();
            result.Success.Should().BeFalse();
        }
        [Fact]
        public void ValidUsernameValidNotExpiredTokenInvalidNewPasswordReturnsFalse()
        {
            // Arrange
            var tok = Values.ValidRecoveryToken;
            var u = Values.UserSpongeBob;
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                u.UserRecoveryTokens.Add(tok);
                model.Users.Add(u);
            });

            // Act
            var result = util.Container.Resolve<IUserRecoveryService>().ResetPassword(Values.UsernameSpongeBob, tok.Token, Values.PasswordInvalidChar);

            // Assert
            result.Token.Should().BeNull();
            result.Success.Should().BeFalse();
        }
        [Fact]
        public void ValidUsernameValidNotExpiredToken()
        {
            // Arrange
            var tok = Values.ValidRecoveryToken;
            var originalTime = tok.Expiration;
            var u = Values.UserSpongeBob;
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                u.UserRecoveryTokens.Add(tok);
                model.Users.Add(u);
                model.Users.Add(Values.UserDora);
            });

            // Act
            var result = util.Container.Resolve<IUserRecoveryService>().ResetPassword(Values.UsernameSpongeBob, tok.Token, Values.PasswordSpongeBobNewOne);

            // Assert
            result.Token.Should().NotBeNull();
            tok.Expiration.Should().BeBefore(originalTime);
            Values.VerifyPassword(Values.PasswordSpongeBobNewOne, u).Should().BeTrue();
        }
    }
}
