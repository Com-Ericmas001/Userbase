using Com.Ericmas001.Userbase.Test.Util;
using NUnit.Framework;

namespace Com.Ericmas001.Userbase.Test
{
    [TestFixture]
    public class EmailExistsTest
    {
        [Test]
        public void WithNoUserReturnsFalse()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator:() => Values.Context );

            // Act
            var result = UserbaseSystem.EmailExists(Values.EmailSpongeBob);

            // Assert
            Assert.IsFalse(result);
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
            var result = UserbaseSystem.EmailExists(Values.EmailDora);

            // Assert
            Assert.IsFalse(result);
        }
        [Test]
        public void WithValidUserReturnsTrue()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                model.Users.Add(Values.UserSpongeBob);
                return model;
            });


            // Act
            var result = UserbaseSystem.EmailExists(Values.EmailSpongeBob);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
