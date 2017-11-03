using System.Linq;
using Com.Ericmas001.Userbase.Models.ServiceInterfaces;
using Com.Ericmas001.Userbase.Test.Util;
using FluentAssertions;
using Unity;
using Xunit;

namespace Com.Ericmas001.Userbase.Test
{
    [Collection("Com.Ericmas001.Userbase.Test")]
    public class CreateUserTest
    {
        [Fact]
        public void WithExistingUsernameReturnsNull()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate(IUserbaseDbContext model)
            {
                model.Users.Add(Values.UserDora);
            });
            // Act
            var result = util.Container.Resolve<IUserManagingService>().CreateUser(Values.UnregisteredDora);
            
            // Assert
            result.Token.Should().BeNull();
            result.Success.Should().BeFalse();
        }
        [Fact]
        public void WithNotFoundUsernameButInvalidUsernameReturnsNull()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                model.Users.Add(Values.UserSpongeBob);
            });

            var userToRegister = Values.UnregisteredDora;
            userToRegister.Username = Values.UsernameTooShort;

            // Act
            var result = util.Container.Resolve<IUserManagingService>().CreateUser(userToRegister);

            // Assert
            result.Token.Should().BeNull();
            result.Success.Should().BeFalse();
        }
        [Fact]
        public void WithNotFoundUsernameButInvalidDisplayNameReturnsNull()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                model.Users.Add(Values.UserSpongeBob);
            });

            var userToRegister = Values.UnregisteredDora;
            userToRegister.Profile.DisplayName = Values.DisplayNameTooShort;

            // Act
            var result = util.Container.Resolve<IUserManagingService>().CreateUser(userToRegister);

            // Assert
            result.Token.Should().BeNull();
            result.Success.Should().BeFalse();
        }
        [Fact]
        public void WithNotFoundUsernameButInvalidPasswordReturnsNull()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                model.Users.Add(Values.UserSpongeBob);
            });

            var userToRegister = Values.UnregisteredDora;
            userToRegister.Authentication.Password = Values.PasswordInvalidChar;

            // Act
            var result = util.Container.Resolve<IUserManagingService>().CreateUser(userToRegister);

            // Assert
            result.Token.Should().BeNull();
            result.Success.Should().BeFalse();
        }
        [Fact]
        public void WithNotFoundUsernameButInvalidEmailReturnsNull()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                model.Users.Add(Values.UserSpongeBob);
            });

            var userToRegister = Values.UnregisteredDora;
            userToRegister.Authentication.Email = Values.EmailNoArobas;

            // Act
            var result = util.Container.Resolve<IUserManagingService>().CreateUser(userToRegister);

            // Assert
            result.Token.Should().BeNull();
            result.Success.Should().BeFalse();
        }
        [Fact]
        public void WithNotFoundUsernameButExistingEmailReturnsNull()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                model.Users.Add(Values.UserSpongeBob);
            });

            var userToRegister = Values.UnregisteredDora;
            userToRegister.Authentication.Email = Values.EmailSpongeBob;

            // Act
            var result = util.Container.Resolve<IUserManagingService>().CreateUser(userToRegister);

            // Assert
            result.Token.Should().BeNull();
            result.Success.Should().BeFalse();
        }
        [Fact]
        public void WithNotFoundUsername()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                model.Users.Add(Values.UserSpongeBob);
            });

            // Act
            var result = util.Container.Resolve<IUserManagingService>().CreateUser(Values.UnregisteredDora);

            // Assert
            result.Token.Should().NotBeNull();
            result.Success.Should().BeTrue();
            util.Model.Users.Should().NotBeEmpty().And.HaveCount(2);

            var dora = util.Model.Users.Single(x => x.Name == Values.UsernameDora);

            dora.Name.Should().BeEquivalentTo(Values.UsernameDora);
            dora.UserProfile.DisplayName.Should().BeEquivalentTo(Values.DisplayNameDora);
            dora.UserAuthentication.RecoveryEmail.Should().BeEquivalentTo(Values.EmailDora);

            Values.VerifyPassword(Values.PasswordDora, dora).Should().BeTrue();
        }
    }
}
