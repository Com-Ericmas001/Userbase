using System.Linq;
using Com.Ericmas001.Userbase.Test.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Com.Ericmas001.Userbase.Test
{
    [TestClass]
    public class CreateUserTest
    {
        [TestMethod]
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
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [TestMethod]
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
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [TestMethod]
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
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [TestMethod]
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
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [TestMethod]
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
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [TestMethod]
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
            Assert.IsNull(result.Token);
            Assert.IsFalse(result.Success);
        }
        [TestMethod]
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
            Assert.IsNotNull(result.Token);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(2, model.Users.Count());
            var dora = model.Users.SingleOrDefault(x => x.Name == Values.UsernameDora);
            Assert.IsNotNull(dora);
            Assert.AreEqual(Values.UsernameDora, dora.Name);
            Assert.AreEqual(Values.DisplayNameDora, dora.UserProfile.DisplayName);
            Assert.AreEqual(Values.EmailDora, dora.UserAuthentication.RecoveryEmail);
            Assert.IsTrue(Values.VerifyPassword(Values.PasswordDora, dora));
        }
    }
}
