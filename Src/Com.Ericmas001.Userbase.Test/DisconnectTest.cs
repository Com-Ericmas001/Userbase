using System;
using Com.Ericmas001.Userbase.Models.ServiceInterfaces;
using Com.Ericmas001.Userbase.Test.Util;
using FluentAssertions;
using Unity;
using Xunit;

namespace Com.Ericmas001.Userbase.Test
{

    [Collection("Com.Ericmas001.Userbase.Test")]
    public class DisconnectTest
    {
        [Fact]
        public void WithInvalidUsernameReturnsFalse()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate{});

            // Act
            var result = util.Container.Resolve<IUserConnectionService>().Disconnect(Values.UsernameSpongeBob, Guid.NewGuid());

            // Assert
            result.Should().BeFalse();
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
            var result = util.Container.Resolve<IUserConnectionService>().Disconnect(Values.UsernameSpongeBob, Guid.NewGuid());

            // Assert
            result.Should().BeFalse();
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
            var result = util.Container.Resolve<IUserConnectionService>().Disconnect(Values.UsernameSpongeBob, Guid.NewGuid());

            // Assert
            result.Should().BeFalse();
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
            var result = util.Container.Resolve<IUserConnectionService>().Disconnect(Values.UsernameSpongeBob, tok.Token);

            // Assert
            result.Should().BeFalse();
        }
        [Fact]
        public void WithValidUsernameValidNotExpiredToken()
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

            // Act
            var result = util.Container.Resolve<IUserConnectionService>().Disconnect(Values.UsernameSpongeBob, tok.Token);

            // Assert
            result.Should().BeTrue();
            tok.Expiration.Should().BeBefore(DateTime.Now);
        }
    }
}
