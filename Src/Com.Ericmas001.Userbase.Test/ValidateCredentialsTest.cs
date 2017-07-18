using Com.Ericmas001.Userbase.Models.ServiceInterfaces;
using Com.Ericmas001.Userbase.Test.Util;
using Microsoft.Practices.Unity;
using Xunit;

namespace Com.Ericmas001.Userbase.Test
{
    public class ValidateCredentialsTest
    {
        [Fact]
        public void WithNoUserReturnsFalse()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate { });

            // Act
            var result = util.Container.Resolve<IUserConnectionService>().ConnectWithPassword(Values.UsernameSpongeBob, Values.PasswordSpongeBob);

            // Assert
            Assert.False(result.Success);
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
            var result = util.Container.Resolve<IUserConnectionService>().ConnectWithPassword(Values.UsernameDora, Values.PasswordDora);

            // Assert
            Assert.False(result.Success);
        }
        [Fact]
        public void WithValidUserWrongPasswordReturnsFalse()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                model.Users.Add(Values.UserSpongeBob);
            });

            // Act
            var result = util.Container.Resolve<IUserConnectionService>().ConnectWithPassword(Values.UsernameSpongeBob, Values.PasswordDumb);

            // Assert
            Assert.False(result.Success);
        }
        [Fact]
        public void WithValidUserValidPasswordReturnsTrue()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                model.Users.Add(Values.UserSpongeBob);
            });

            // Act
            var result = util.Container.Resolve<IUserConnectionService>().ConnectWithPassword(Values.UsernameSpongeBob, Values.PasswordSpongeBob);

            // Assert
            Assert.True(result.Success);
        }
    }
}
