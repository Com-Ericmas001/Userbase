using Com.Ericmas001.Userbase.Test.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Com.Ericmas001.Userbase.Test
{
    [TestClass]
    public class UsernameExistsTest
    {
        [TestMethod]
        public void WithNoUserReturnsFalse()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator:() => Values.Context );

            // Act
            var result = UserbaseSystem.UsernameExists(Values.UsernameSpongeBob);

            // Assert
            Assert.IsFalse(result);
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
            var result = UserbaseSystem.UsernameExists(Values.UsernameDora);

            // Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
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
            var result = UserbaseSystem.UsernameExists(Values.UsernameSpongeBob);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
