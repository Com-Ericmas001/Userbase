using System;
using Com.Ericmas001.Userbase.Models.ServiceInterfaces;
using Com.Ericmas001.Userbase.Test.Util;
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
            var result = util.Container.Resolve<IUserRecoveryService>().ResetPassword(Values.UsernameSpongeBob, Guid.NewGuid().ToString(), Values.PasswordSpongeBobNewOne);

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
                u.UserRecoveryTokens.Add(Values.ValidRecoveryToken);
                model.Users.Add(u);
            });

            // Act
            var result = util.Container.Resolve<IUserRecoveryService>().ResetPassword(Values.UsernameSpongeBob, Guid.NewGuid().ToString(), Values.PasswordSpongeBobNewOne);

            // Assert
            Assert.Null(result.Token);
            Assert.False(result.Success);
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
            Assert.Null(result.Token);
            Assert.False(result.Success);
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
            Assert.Null(result.Token);
            Assert.False(result.Success);
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
            Assert.NotNull(result.Token);
            Assert.True(tok.Expiration < originalTime);
            Assert.True(Values.VerifyPassword(Values.PasswordSpongeBobNewOne, u));
        }
    }
}
