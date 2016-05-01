using System;
using Com.Ericmas001.Userbase.Test.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Com.Ericmas001.Userbase.Test
{
    [TestClass]
    public class IdFromUsernameTest
    {
        [TestMethod]
        public void WithNoUserReturnsNull()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator:() => Values.Context );

            // Act
            var result = UserbaseSystem.IdFromUsername(Values.UsernameSpongeBob);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void WithInvalidUserReturnsNull()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                model.Users.Add(Values.UserSpongeBob);
                return model;
            });

            // Act
            var result = UserbaseSystem.IdFromUsername(Values.UsernameDora);

            // Assert
            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void WithValidUserReturnsUser()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                model.Users.Add(Values.UserSpongeBob);
                return model;
            });


            // Act
            var result = UserbaseSystem.IdFromUsername(Values.UsernameSpongeBob);

            // Assert
            Assert.AreNotEqual(0, result);
            Assert.AreEqual(Values.UserSpongeBob.IdUser, result);
        }
    }
}
