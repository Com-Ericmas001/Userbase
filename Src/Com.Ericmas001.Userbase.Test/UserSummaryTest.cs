using System;
using System.Linq;
using Com.Ericmas001.Userbase.Test.Util;
using Xunit;

namespace Com.Ericmas001.Userbase.Test
{
    public class UserSummaryTest
    {
        [Fact]
        public void WithInvalidUsernameReturnsFalse()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate { });

            // Act
            var result = util.System.UserSummary(Values.UsernameSpongeBob, Guid.NewGuid(), Values.UsernameDora);

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
            var result = util.System.UserSummary(Values.UsernameSpongeBob, Guid.NewGuid(), Values.UsernameDora);

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
            var result = util.System.UserSummary(Values.UsernameSpongeBob, Guid.NewGuid(), Values.UsernameDora);

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
            var result = util.System.UserSummary(Values.UsernameSpongeBob, tok.Token, Values.UsernameDora);

            // Assert
            Assert.Null(result.Token);
            Assert.False(result.Success);
        }
        [Fact]
        public void WithValidUsernameValidNotExpiredTokenButInvalidRequestedUsernameReturnsFalse()
        {
            // Arrange
            var tok = Values.ValidToken;
            var u = Values.UserSpongeBob;
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                u.UserTokens.Add(tok);
                model.Users.Add(u);
            });

            // Act
            var result = util.System.UserSummary(Values.UsernameSpongeBob, tok.Token, Values.UsernameDora);

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
            var result = util.System.UserSummary(Values.UsernameSpongeBob, tok.Token, Values.UsernameDora);

            // Assert
            Assert.NotNull(result.Token);
            Assert.True(tok.Expiration > originalTime);
            Assert.Equal(Values.DisplayNameDora, result.DisplayName);
            Assert.Equal(1, result.Groups.Count());
            Assert.Equal(Values.GroupDummyId, result.Groups.First().Id);
            Assert.Equal(Values.GroupDummyName, result.Groups.First().Name);
        }
    }
}
