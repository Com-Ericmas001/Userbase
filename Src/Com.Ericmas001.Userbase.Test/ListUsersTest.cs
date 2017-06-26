using System;
using System.Linq;
using Com.Ericmas001.Userbase.Test.Util;
using Xunit;

namespace Com.Ericmas001.Userbase.Test
{
    [Collection("Com.Ericmas001.Userbase.Test")]
    public class ListUsersTest
    {
        [Fact]
        public void WithInvalidUsernameReturnsFalse()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: () => Values.Context);

            // Act
            var result = UserbaseSystem.ListUsers(Values.UsernameSpongeBob, Guid.NewGuid());

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
            var result = UserbaseSystem.ListUsers(Values.UsernameSpongeBob, Guid.NewGuid());

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
            var result = UserbaseSystem.ListUsers(Values.UsernameSpongeBob, Guid.NewGuid());

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
            var result = UserbaseSystem.ListUsers(Values.UsernameSpongeBob, tok.Token);

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
                model.Users.Add(Values.UserDora);
                return model;
            });


            // Act
            var result = UserbaseSystem.ListUsers(Values.UsernameSpongeBob, tok.Token);

            // Assert
            Assert.NotNull(result.Token);
            Assert.True(tok.Expiration > originalTime);
            Assert.Equal(2, result.Users.Count());

            Assert.Equal(Values.IdUserSpongeBob, result.Users.First().IdUser);
            Assert.Equal(Values.UsernameSpongeBob, result.Users.First().Username);
            Assert.Equal(Values.DisplayNameSpongeBob, result.Users.First().DisplayName);
            Assert.Equal(2, result.Users.First().Groups.Count);
            Assert.Equal(Values.GroupAdminId, result.Users.First().Groups.First().Key);
            Assert.Equal(Values.GroupAdminName, result.Users.First().Groups.First().Value);
            Assert.Equal(Values.GroupDummyId, result.Users.First().Groups.Last().Key);
            Assert.Equal(Values.GroupDummyName, result.Users.First().Groups.Last().Value);

            Assert.Equal(Values.IdUserDora, result.Users.Last().IdUser);
            Assert.Equal(Values.UsernameDora, result.Users.Last().Username);
            Assert.Equal(Values.DisplayNameDora, result.Users.Last().DisplayName);
            Assert.Equal(1, result.Users.Last().Groups.Count);
            Assert.Equal(Values.GroupDummyId, result.Users.Last().Groups.First().Key);
            Assert.Equal(Values.GroupDummyName, result.Users.Last().Groups.First().Value);
        }
    }
}
