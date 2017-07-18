using Com.Ericmas001.Userbase.Models.ServiceInterfaces;
using Com.Ericmas001.Userbase.Test.Util;
using Microsoft.Practices.Unity;
using Xunit;

namespace Com.Ericmas001.Userbase.Test
{
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
            Assert.Equal(0, result);
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
            Assert.Equal(0, result);
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
            Assert.NotEqual(0, result);
            Assert.Equal(Values.UserSpongeBob.IdUser, result);
        }
    }
}
