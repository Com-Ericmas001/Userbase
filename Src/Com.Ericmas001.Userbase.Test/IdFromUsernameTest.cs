//using Com.Ericmas001.Userbase.Test.Util;
//using Xunit;

//namespace Com.Ericmas001.Userbase.Test
//{
//    [Collection("Com.Ericmas001.Userbase.Test")]
//    public class IdFromUsernameTest
//    {
//        [Fact]
//        public void WithNoUserReturnsZero()
//        {
//            // Arrange
//            UserbaseSystem.Init(Values.Salt, contextGenerator:() => Values.Context );

//            // Act
//            var result = UserbaseSystem.IdFromUsername(Values.UsernameSpongeBob);

//            // Assert
//            Assert.Equal(0, result);
//        }

//        [Fact]
//        public void WithInvalidUserReturnsZero()
//        {
//            // Arrange
//            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
//            {
//                var model = Values.Context;
//                model.Users.Add(Values.UserSpongeBob);
//                return model;
//            });

//            // Act
//            var result = UserbaseSystem.IdFromUsername(Values.UsernameDora);

//            // Assert
//            Assert.Equal(0, result);
//        }
//        [Fact]
//        public void WithValidUserReturnsId()
//        {
//            // Arrange
//            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
//            {
//                var model = Values.Context;
//                model.Users.Add(Values.UserSpongeBob);
//                return model;
//            });


//            // Act
//            var result = UserbaseSystem.IdFromUsername(Values.UsernameSpongeBob);

//            // Assert
//            Assert.NotEqual(0, result);
//            Assert.Equal(Values.UserSpongeBob.IdUser, result);
//        }
//    }
//}
