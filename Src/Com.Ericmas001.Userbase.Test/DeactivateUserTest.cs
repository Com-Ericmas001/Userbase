using System;
using Com.Ericmas001.Userbase.Models.ServiceInterfaces;
using Com.Ericmas001.Userbase.Test.Util;
using FluentAssertions;
using Unity;
using Xunit;

namespace Com.Ericmas001.Userbase.Test
{

    [Collection("Com.Ericmas001.Userbase.Test")]
    public class DeactivateUserTest
    {
        [Fact]
        public void WithNoUserReturnsFalse()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate { });

            // Act
            var result = util.Container.Resolve<IUserManagingService>().Deactivate(Values.UsernameSpongeBob, Values.ValidToken.Token);

            // Assert
            result.Should().BeFalse();
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
            var result = util.Container.Resolve<IUserManagingService>().Deactivate(Values.UsernameDora, Values.ValidToken.Token);

            // Assert
            result.Should().BeFalse();
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
            var result = util.Container.Resolve<IUserManagingService>().Deactivate(Values.UsernameSpongeBob, Values.ValidToken.Token);

            // Assert
            result.Should().BeFalse();
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
            var result = util.Container.Resolve<IUserManagingService>().Deactivate(Values.UsernameSpongeBob, Guid.NewGuid());

            // Assert
            result.Should().BeFalse();
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
            var result = util.Container.Resolve<IUserManagingService>().Deactivate(Values.UsernameSpongeBob, tok.Token);

            // Assert
            result.Should().BeFalse();
        }
        [Fact]
        public void WithValidUserValidTokenReturnsTrue()
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
            var result = util.Container.Resolve<IUserManagingService>().Deactivate(Values.UsernameSpongeBob, tok.Token);

            // Assert
            result.Should().BeTrue();
            u.Active.Should().BeFalse();
        }
    }
}
