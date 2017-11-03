using Com.Ericmas001.Userbase.Models.ServiceInterfaces;
using Com.Ericmas001.Userbase.Test.Util;
using FluentAssertions;
using Unity;
using Xunit;

namespace Com.Ericmas001.Userbase.Test
{

    [Collection("Com.Ericmas001.Userbase.Test")]
    public class IdFromUsernameTest
    {
        [Fact]
        public void WithNoUserReturnsZero()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate{});

            // Act
            var result = util.Container.Resolve<IUserObtentionService>().FromUsername(Values.UsernameSpongeBob);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void WithInvalidUserReturnsZero()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                model.Users.Add(Values.UserSpongeBob);
            });

            // Act
            var result = util.Container.Resolve<IUserObtentionService>().FromUsername(Values.UsernameDora);

            // Assert
            result.Should().Be(0);
        }
        [Fact]
        public void WithValidUserReturnsId()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                model.Users.Add(Values.UserSpongeBob);
            });

            // Act
            var result = util.Container.Resolve<IUserObtentionService>().FromUsername(Values.UsernameSpongeBob);

            // Assert
            result.Should().Be(Values.UserSpongeBob.IdUser);
        }
    }
}
