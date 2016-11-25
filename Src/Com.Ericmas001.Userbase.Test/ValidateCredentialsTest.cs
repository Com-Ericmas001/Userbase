using Com.Ericmas001.Userbase.Test.Util;
using NUnit.Framework;

namespace Com.Ericmas001.Userbase.Test
{
    [TestFixture]
    public class ValidateCredentialsTest
    {
        [Test]
        public void WithNoUserReturnsFalse()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator:() => Values.Context );

            // Act
            var result = UserbaseSystem.ValidateCredentials(Values.UsernameSpongeBob, Values.PasswordSpongeBob);

            // Assert
            Assert.IsFalse(result.Success);
        }

        [Test]
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
        [Test]
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
        [Test]
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
