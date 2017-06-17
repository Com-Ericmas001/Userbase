using System.Linq;
using Com.Ericmas001.Userbase.Test.Util;
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
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                model.Users.Add(Values.UserDora);
                return model;
            });

            // Act
            var result = UserbaseSystem.CreateUser(Values.UnregisteredDora);
            
            // Assert
            Assert.Null(result.Token);
            Assert.False(result.Success);
        }
        [Fact]
        public void WithNotFoundUsernameButInvalidUsernameReturnsNull()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                model.Users.Add(Values.UserSpongeBob);
                return model;
            });

            var userToRegister = Values.UnregisteredDora;
            userToRegister.Username = Values.UsernameTooShort;

            // Act
            var result = UserbaseSystem.CreateUser(userToRegister);

            // Assert
            Assert.Null(result.Token);
            Assert.False(result.Success);
        }
        [Fact]
        public void WithNotFoundUsernameButInvalidDisplayNameReturnsNull()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                model.Users.Add(Values.UserSpongeBob);
                return model;
            });

            var userToRegister = Values.UnregisteredDora;
            userToRegister.Profile.DisplayName = Values.DisplayNameTooShort;

            // Act
            var result = UserbaseSystem.CreateUser(userToRegister);

            // Assert
            Assert.Null(result.Token);
            Assert.False(result.Success);
        }
        [Fact]
        public void WithNotFoundUsernameButInvalidPasswordReturnsNull()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                model.Users.Add(Values.UserSpongeBob);
                return model;
            });

            var userToRegister = Values.UnregisteredDora;
            userToRegister.Authentication.Password = Values.PasswordInvalidChar;

            // Act
            var result = UserbaseSystem.CreateUser(userToRegister);

            // Assert
            Assert.Null(result.Token);
            Assert.False(result.Success);
        }
        [Fact]
        public void WithNotFoundUsernameButInvalidEmailReturnsNull()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                model.Users.Add(Values.UserSpongeBob);
                return model;
            });

            var userToRegister = Values.UnregisteredDora;
            userToRegister.Authentication.Email = Values.EmailNoArobas;

            // Act
            var result = UserbaseSystem.CreateUser(userToRegister);

            // Assert
            Assert.Null(result.Token);
            Assert.False(result.Success);
        }
        [Fact]
        public void WithNotFoundUsernameButExistingEmailReturnsNull()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                model.Users.Add(Values.UserSpongeBob);
                return model;
            });

            var userToRegister = Values.UnregisteredDora;
            userToRegister.Authentication.Email = Values.EmailSpongeBob;

            // Act
            var result = UserbaseSystem.CreateUser(userToRegister);

            // Assert
            Assert.Null(result.Token);
            Assert.False(result.Success);
        }
        [Fact]
        public void WithNotFoundUsername()
        {
            var model = Values.Context;
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                model.Users.Add(Values.UserSpongeBob);
                return model;
            });

            // Act
            var result = UserbaseSystem.CreateUser(Values.UnregisteredDora);

            // Assert
            Assert.NotNull(result.Token);
            Assert.True(result.Success);
            Assert.Equal(2, model.Users.Count());
            var dora = model.Users.SingleOrDefault(x => x.Name == Values.UsernameDora);
            Assert.NotNull(dora);
            Assert.Equal(Values.UsernameDora, dora.Name);
            Assert.Equal(Values.DisplayNameDora, dora.UserProfile.DisplayName);
            Assert.Equal(Values.EmailDora, dora.UserAuthentication.RecoveryEmail);
            Assert.True(Values.VerifyPassword(Values.PasswordDora, dora));
        }
    }
}
