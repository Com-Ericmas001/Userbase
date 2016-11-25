using Com.Ericmas001.Userbase.Test.Util;
using NUnit.Framework;

namespace Com.Ericmas001.Userbase.Test
{
    [TestFixture]
    public class IdFromEmailTest
    {
        [Test]
        public void WithNoUserReturnsZero()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator:() => Values.Context );

            // Act
            var result = UserbaseSystem.IdFromEmail(Values.EmailSpongeBob);

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void WithInvalidUserReturnsZero()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                model.Users.Add(Values.UserSpongeBob);
                return model;
            });

            // Act
            var result = UserbaseSystem.IdFromEmail(Values.EmailDora);

            // Assert
            Assert.AreEqual(0, result);
        }
        [Test]
        public void WithValidUserReturnsId()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                model.Users.Add(Values.UserSpongeBob);
                return model;
            });


            // Act
            var result = UserbaseSystem.IdFromEmail(Values.EmailSpongeBob);

            // Assert
            Assert.AreNotEqual(0, result);
            Assert.AreEqual(Values.UserSpongeBob.IdUser, result);
        }
    }
}
