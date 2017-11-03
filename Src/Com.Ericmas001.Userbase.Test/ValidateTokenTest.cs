using System;
using Com.Ericmas001.Userbase.Models.ServiceInterfaces;
using Com.Ericmas001.Userbase.Test.Util;
using FluentAssertions;
using Unity;
using Xunit;

namespace Com.Ericmas001.Userbase.Test
{

    [Collection("Com.Ericmas001.Userbase.Test")]
    public class ValidateTokenTest
    {
        [Fact]
        public void WithNoUserReturnsFalse()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate { });

            // Act
            var result = util.Container.Resolve<IUserConnectionService>().ConnectWithToken(Values.UsernameSpongeBob, Values.ValidToken.Token);

            // Assert
            result.Success.Should().BeFalse();
        }

        [Fact]
        public void WithInvalidUserReturnsFalse()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                model.Users.Add(Values.UserSpongeBob);
            });

            // Act
            var result = util.Container.Resolve<IUserConnectionService>().ConnectWithToken(Values.UsernameDora, Values.ValidToken.Token);

            // Assert
            result.Success.Should().BeFalse();
        }
        [Fact]
        public void WithValidUserNoTokensReturnsFalse()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                model.Users.Add(Values.UserSpongeBob);
            });

            // Act
            var result = util.Container.Resolve<IUserConnectionService>().ConnectWithToken(Values.UsernameSpongeBob, Values.ValidToken.Token);

            // Assert
            result.Success.Should().BeFalse();
        }
        [Fact]
        public void WithValidUserInvalidTokenReturnsFalse()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                var u = Values.UserSpongeBob;
                u.UserTokens.Add(Values.ValidToken);
                model.Users.Add(u);
            });

            // Act
            var result = util.Container.Resolve<IUserConnectionService>().ConnectWithToken(Values.UsernameSpongeBob, Guid.NewGuid());

            // Assert
            result.Success.Should().BeFalse();
        }
        [Fact]
        public void WithValidUserExpiredTokenReturnsFalse()
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
            var result = util.Container.Resolve<IUserConnectionService>().ConnectWithToken(Values.UsernameSpongeBob, tok.Token);

            // Assert
            result.Success.Should().BeFalse();
        }
        [Fact]
        public void WithValidUserValidTokenReturnsTrue()
        {
            // Arrange
            var tok = Values.ValidToken;
            var originalExpiration = tok.Expiration;
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                var u = Values.UserSpongeBob;
                u.UserTokens.Add(tok);
                model.Users.Add(u);
            });

            // Act
            var result = util.Container.Resolve<IUserConnectionService>().ConnectWithToken(Values.UsernameSpongeBob, tok.Token);

            // Assert
            result.Success.Should().BeTrue();
            result.Token.Id.Should().Be(tok.Token);
            result.Token.ValidUntil.Should().BeAfter(originalExpiration);
        }
    }
}
