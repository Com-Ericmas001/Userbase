using System;
using Com.Ericmas001.Userbase.Models.Requests;
using Com.Ericmas001.Userbase.Models.ServiceInterfaces;
using Com.Ericmas001.Userbase.Test.Util;
using Microsoft.Practices.Unity;
using Xunit;

namespace Com.Ericmas001.Userbase.Test
{
    public class ModifyProfileTest
    {
        [Fact]
        public void WithInvalidUsernameReturnsFalse()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate {});

            // Act
            var result = util.Container.Resolve<IUserManagingService>().ModifyProfile(new ModifyProfileRequest
            {
                Username = Values.UsernameSpongeBob,
                Token = Guid.NewGuid(),
                Profile = Values.NewProfileSpongeBob
            });

            // Assert
            Assert.Null(result.Token);
            Assert.False(result.Success);
        }
        [Fact]
        public void WithValidUsernameButNoTokenReturnsFalse()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                model.Users.Add(Values.UserSpongeBob);
            });

            // Act
            var result = util.Container.Resolve<IUserManagingService>().ModifyProfile(new ModifyProfileRequest
            {
                Username = Values.UsernameSpongeBob,
                Token = Guid.NewGuid(),
                Profile = Values.NewProfileSpongeBob
            });

            // Assert
            Assert.Null(result.Token);
            Assert.False(result.Success);
        }
        [Fact]
        public void WithValidUsernameButInvalidTokenReturnsFalse()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                var u = Values.UserSpongeBob;
                u.UserTokens.Add(Values.ValidToken);
                model.Users.Add(u);
            });

            // Act
            var result = util.Container.Resolve<IUserManagingService>().ModifyProfile(new ModifyProfileRequest
            {
                Username = Values.UsernameSpongeBob,
                Token = Guid.NewGuid(),
                Profile = Values.NewProfileSpongeBob
            });

            // Assert
            Assert.Null(result.Token);
            Assert.False(result.Success);
        }
        [Fact]
        public void WithValidUsernameButExpiredTokenReturnsFalse()
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
            var result = util.Container.Resolve<IUserManagingService>().ModifyProfile(new ModifyProfileRequest
            {
                Username = Values.UsernameSpongeBob,
                Token = tok.Token,
                Profile = Values.NewProfileSpongeBob
            });

            // Assert
            Assert.Null(result.Token);
            Assert.False(result.Success);
        }
        [Fact]
        public void WithValidUsernameValidNotExpiredTokenButInvalidDisplayNameReturnsFalse()
        {
            // Arrange
            var tok = Values.ValidToken;
            var u = Values.UserSpongeBob;
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                u.UserTokens.Add(tok);
                model.Users.Add(u);
            });

            var prof = Values.NewProfileSpongeBob;
            prof.DisplayName = Values.DisplayNameTooShort;

            // Act
            var result = util.Container.Resolve<IUserManagingService>().ModifyProfile(new ModifyProfileRequest
            {
                Username = Values.UsernameSpongeBob,
                Token = tok.Token,
                Profile = prof
            });

            // Assert
            Assert.Null(result.Token);
            Assert.False(result.Success);
        }
        [Fact]
        public void WithValidUsernameValidNotExpiredToken()
        {
            // Arrange
            var tok = Values.ValidToken;
            var originalTime = tok.Expiration;
            var u = Values.UserSpongeBob;
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                u.UserTokens.Add(tok);
                model.Users.Add(u);
            });

            // Act
            var result = util.Container.Resolve<IUserManagingService>().ModifyProfile(new ModifyProfileRequest
            {
                Username = Values.UsernameSpongeBob,
                Token = tok.Token,
                Profile = Values.NewProfileSpongeBob
            });

            // Assert
            Assert.NotNull(result.Token);
            Assert.True(tok.Expiration > originalTime);
            Assert.Equal(Values.DisplayNameSpongeBobNewOne, u.UserProfile.DisplayName);
        }
    }
}
