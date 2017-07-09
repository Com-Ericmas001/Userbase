﻿//using Com.Ericmas001.Userbase.Test.Util;
//using Xunit;

//namespace Com.Ericmas001.Userbase.Test
//{
//    [Collection("Com.Ericmas001.Userbase.Test")]
//    public class ValidateCredentialsTest
//    {
//        [Fact]
//        public void WithNoUserReturnsFalse()
//        {
//            // Arrange
//            UserbaseSystem.Init(Values.Salt, contextGenerator:() => Values.Context );

//            // Act
//            var result = UserbaseSystem.ValidateCredentials(Values.UsernameSpongeBob, Values.PasswordSpongeBob);

//            // Assert
//            Assert.False(result.Success);
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
//            var result = UserbaseSystem.ValidateCredentials(Values.UsernameDora, Values.PasswordDora);

//            // Assert
//            Assert.False(result.Success);
//        }
//        [Fact]
//        public void WithValidUserWrongPasswordReturnsFalse()
//        {
//            // Arrange
//            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
//            {
//                var model = Values.Context;
//                model.Users.Add(Values.UserSpongeBob);
//                return model;
//            });

//            // Act
//            var result = UserbaseSystem.ValidateCredentials(Values.UsernameSpongeBob, Values.PasswordDumb);

//            // Assert
//            Assert.False(result.Success);
//        }
//        [Fact]
//        public void WithValidUserValidPasswordReturnsTrue()
//        {
//            // Arrange
//            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
//            {
//                var model = Values.Context;
//                model.Users.Add(Values.UserSpongeBob);
//                return model;
//            });

//            // Act
//            var result = UserbaseSystem.ValidateCredentials(Values.UsernameSpongeBob, Values.PasswordSpongeBob);

//            // Assert
//            Assert.True(result.Success);
//        }
//    }
//}
