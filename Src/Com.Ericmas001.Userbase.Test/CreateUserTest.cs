using System.Linq;
using Com.Ericmas001.Userbase.Models.ServiceInterfaces;
using Com.Ericmas001.Userbase.Test.Util;
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
            Assert.Null(result.Token);
            Assert.False(result.Success);
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
            Assert.Null(result.Token);
            Assert.False(result.Success);
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
            Assert.Null(result.Token);
            Assert.False(result.Success);
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
            Assert.Null(result.Token);
            Assert.False(result.Success);
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
            Assert.Null(result.Token);
            Assert.False(result.Success);
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
            Assert.Null(result.Token);
            Assert.False(result.Success);
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
            Assert.NotNull(result.Token);
            Assert.True(result.Success);
            Assert.Equal(2, util.Model.Users.Count());
            var dora = util.Model.Users.SingleOrDefault(x => x.Name == Values.UsernameDora);
            Assert.NotNull(dora);
            Assert.Equal(Values.UsernameDora, dora.Name);
            Assert.Equal(Values.DisplayNameDora, dora.UserProfile.DisplayName);
            Assert.Equal(Values.EmailDora, dora.UserAuthentication.RecoveryEmail);
            Assert.True(Values.VerifyPassword(Values.PasswordDora, dora));
        }
    }
}
