using System;
using System.Linq;
using Com.Ericmas001.Userbase.Test.Util;
using Xunit;

namespace Com.Ericmas001.Userbase.Test
{
    public class ListUsersTest
    {
        [Fact]
        public void WithInvalidUsernameReturnsFalse()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate { });

            // Act
            var result = util.System.ListUsers(Values.UsernameSpongeBob, Guid.NewGuid());

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
            var result = util.System.ListUsers(Values.UsernameSpongeBob, Guid.NewGuid());

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
            var result = util.System.ListUsers(Values.UsernameSpongeBob, Guid.NewGuid());

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
            var result = util.System.ListUsers(Values.UsernameSpongeBob, tok.Token);

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
                model.Users.Add(Values.UserDora);
            });


            // Act
            var result = util.System.ListUsers(Values.UsernameSpongeBob, tok.Token);

            // Assert
            Assert.NotNull(result.Token);
            Assert.True(tok.Expiration > originalTime);
            Assert.Equal(2, result.Users.Count());

            Assert.Equal(Values.IdUserSpongeBob, result.Users.First().IdUser);
            Assert.Equal(Values.UsernameSpongeBob, result.Users.First().Username);
            Assert.Equal(Values.DisplayNameSpongeBob, result.Users.First().DisplayName);
            Assert.Equal(2, result.Users.First().Groups.Count());
            Assert.Equal(Values.GroupAdminId, result.Users.First().Groups.First().Id);
            Assert.Equal(Values.GroupAdminName, result.Users.First().Groups.First().Name);
            Assert.Equal(Values.GroupDummyId, result.Users.First().Groups.Last().Id);
            Assert.Equal(Values.GroupDummyName, result.Users.First().Groups.Last().Name);

            Assert.Equal(Values.IdUserDora, result.Users.Last().IdUser);
            Assert.Equal(Values.UsernameDora, result.Users.Last().Username);
            Assert.Equal(Values.DisplayNameDora, result.Users.Last().DisplayName);
            Assert.Equal(1, result.Users.Last().Groups.Count());
            Assert.Equal(Values.GroupDummyId, result.Users.Last().Groups.First().Id);
            Assert.Equal(Values.GroupDummyName, result.Users.Last().Groups.First().Name);
        }
    }
}
