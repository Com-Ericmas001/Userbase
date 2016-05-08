using Com.Ericmas001.Userbase.Test.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Com.Ericmas001.Userbase.Test
{
    [TestClass]
    public class ValidateCredentialsTest
    {
        [TestMethod]
        public void WithNoUserReturnsFalse()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator:() => Values.Context );

            // Act
            var result = UserbaseSystem.ValidateCredentials(Values.UsernameSpongeBob, Values.PasswordSpongeBob);

            // Assert
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void WithInvalidUserReturnsFalse()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                model.Users.Add(Values.UserSpongeBob);
                return model;
            });

            // Act
            var result = UserbaseSystem.ValidateCredentials(Values.UsernameDora, Values.PasswordDora);

            // Assert
            Assert.IsFalse(result.Success);
        }
        [TestMethod]
        public void WithValidUserWrongPasswordReturnsFalse()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                model.Users.Add(Values.UserSpongeBob);
                return model;
            });

            // Act
            var result = UserbaseSystem.ValidateCredentials(Values.UsernameSpongeBob, Values.PasswordDumb);

            // Assert
            Assert.IsFalse(result.Success);
        }
        [TestMethod]
        public void WithValidUserValidPasswordReturnsTrue()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                model.Users.Add(Values.UserSpongeBob);
                return model;
            });

            // Act
            var result = UserbaseSystem.ValidateCredentials(Values.UsernameSpongeBob, Values.PasswordSpongeBob);

            // Assert
            Assert.IsTrue(result.Success);
        }
    }
}
