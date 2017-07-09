//using Com.Ericmas001.Userbase.Test.Util;
//using Xunit;

//namespace Com.Ericmas001.Userbase.Test
//{
//    [Collection("Com.Ericmas001.Userbase.Test")]
//    public class UsernameExistsTest
//    {
//        [Fact]
//        public void WithNoUserReturnsFalse()
//        {
//            // Arrange
//            UserbaseSystem.Init(Values.Salt, contextGenerator:() => Values.Context );

//            // Act
//            var result = UserbaseSystem.UsernameExists(Values.UsernameSpongeBob);

//            // Assert
//            Assert.False(result);
//        }

//        [Fact]
//        public void WithInvalidUserReturnsFalse()
//        {
//            // Arrange
//            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
//            {
//                var model = Values.Context;
//                model.Users.Add(Values.UserSpongeBob);
//                return model;
//            });

//            // Act
//            var result = UserbaseSystem.UsernameExists(Values.UsernameDora);

//            // Assert
//            Assert.False(result);
//        }
//        [Fact]
//        public void WithValidUserReturnsTrue()
//        {
//            // Arrange
//            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
//            {
//                var model = Values.Context;
//                model.Users.Add(Values.UserSpongeBob);
//                return model;
//            });


//            // Act
//            var result = UserbaseSystem.UsernameExists(Values.UsernameSpongeBob);

//            // Assert
//            Assert.True(result);
//        }
//    }
//}
