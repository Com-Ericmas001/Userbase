using System;
using Com.Ericmas001.Userbase.Requests;
using Com.Ericmas001.Userbase.Test.Util;
using NUnit.Framework;

namespace Com.Ericmas001.Userbase.Test
{
    [TestFixture]
    public class ModifyProfileTest
    {
        [Test]
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
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [Test]
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
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [Test]
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
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [Test]
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
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [Test]
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
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [Test]
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
            Assert.IsNotNull(result.Token);
            Assert.IsTrue(tok.Expiration > originalTime);
            Assert.AreEqual(Values.DisplayNameSpongeBobNewOne, u.UserProfile.DisplayName);
        }
    }
}
