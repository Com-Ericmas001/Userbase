using System;
using System.Linq;
using Com.Ericmas001.Userbase.Models.ServiceInterfaces;
using Com.Ericmas001.Userbase.Test.Util;
using FluentAssertions;
using Unity;
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
            var util = new UserbaseSystemUtil(delegate { });

            // Act
            var result = util.Container.Resolve<IUserInformationService>().ListAllUsers(Values.UsernameSpongeBob, Guid.NewGuid());

            // Assert
            result.Token.Should().BeNull();
            result.Success.Should().BeFalse();
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
            var result = util.Container.Resolve<IUserInformationService>().ListAllUsers(Values.UsernameSpongeBob, Guid.NewGuid());

            // Assert
            result.Token.Should().BeNull();
            result.Success.Should().BeFalse();
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
            var result = util.Container.Resolve<IUserInformationService>().ListAllUsers(Values.UsernameSpongeBob, Guid.NewGuid());

            // Assert
            result.Token.Should().BeNull();
            result.Success.Should().BeFalse();
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
            var result = util.Container.Resolve<IUserInformationService>().ListAllUsers(Values.UsernameSpongeBob, tok.Token);

            // Assert
            result.Token.Should().BeNull();
            result.Success.Should().BeFalse();
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
            var result = util.Container.Resolve<IUserInformationService>().ListAllUsers(Values.UsernameSpongeBob, tok.Token);

            // Assert
            result.Token.Should().NotBeNull();
            tok.Expiration.Should().BeAfter(originalTime);
            result.Users.Count().Should().Be(2);

            result.Users.First().IdUser.Should().Be(Values.IdUserSpongeBob);
            result.Users.First().Username.Should().Be(Values.UsernameSpongeBob);
            result.Users.First().DisplayName.Should().Be(Values.DisplayNameSpongeBob);
            result.Users.First().Groups.Count().Should().Be(2);
            result.Users.First().Groups.First().Id.Should().Be(Values.GroupAdminId);
            result.Users.First().Groups.First().Name.Should().Be(Values.GroupAdminName);
            result.Users.First().Groups.Last().Id.Should().Be(Values.GroupDummyId);
            result.Users.First().Groups.Last().Name.Should().Be(Values.GroupDummyName);

            result.Users.Last().IdUser.Should().Be(Values.IdUserDora);
            result.Users.Last().Username.Should().Be(Values.UsernameDora);
            result.Users.Last().DisplayName.Should().Be(Values.DisplayNameDora);
            result.Users.Last().Groups.Count().Should().Be(1);
            result.Users.Last().Groups.First().Id.Should().Be(Values.GroupDummyId);
            result.Users.Last().Groups.First().Name.Should().Be(Values.GroupDummyName);
        }
    }
}
