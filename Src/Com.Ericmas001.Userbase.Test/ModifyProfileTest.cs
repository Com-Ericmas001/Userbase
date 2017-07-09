using System;
using Com.Ericmas001.Userbase.Models.Requests;
using Com.Ericmas001.Userbase.Test.Util;
using Xunit;

namespace Com.Ericmas001.Userbase.Test
{
    [Collection("Com.Ericmas001.Userbase.Test")]
    public class ModifyProfileTest
    {
        [Fact]
        public void WithInvalidUsernameReturnsFalse()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: () => Values.Context);

            // Act
            var result = UserbaseSystem.ModifyProfile(new ModifyProfileRequest
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
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                model.Users.Add(Values.UserSpongeBob);
                return model;
            });

            // Act
            var result = UserbaseSystem.ModifyProfile(new ModifyProfileRequest
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
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                var u = Values.UserSpongeBob;
                u.UserTokens.Add(Values.ValidToken);
                model.Users.Add(u);
                return model;
            });

            // Act
            var result = UserbaseSystem.ModifyProfile(new ModifyProfileRequest
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
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                var u = Values.UserSpongeBob;
                u.UserTokens.Add(tok);
                model.Users.Add(u);
                return model;
            });

            // Act
            var result = UserbaseSystem.ModifyProfile(new ModifyProfileRequest
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
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                u.UserTokens.Add(tok);
                model.Users.Add(u);
                return model;
            });

            var prof = Values.NewProfileSpongeBob;
            prof.DisplayName = Values.DisplayNameTooShort;

            // Act
            var result = UserbaseSystem.ModifyProfile(new ModifyProfileRequest
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
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                u.UserTokens.Add(tok);
                model.Users.Add(u);
                return model;
            });

            // Act
            var result = UserbaseSystem.ModifyProfile(new ModifyProfileRequest
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
