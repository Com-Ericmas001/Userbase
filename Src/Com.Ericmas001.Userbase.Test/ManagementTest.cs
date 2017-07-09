//using System.Linq;
//using Com.Ericmas001.Userbase.Test.Util;
//using Xunit;

//namespace Com.Ericmas001.Userbase.Test
//{
//    [Collection("Com.Ericmas001.Userbase.Test")]
//    public class ManagementTest
//    {

//        [Fact]
//        public void PurgeConnectionTokens()
//        {
//            // Arrange

//            var model = Values.Context;
//            var expiredToken = Values.ExpiredToken;
//            var validToken = Values.ValidToken;
//            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
//            {
//                model.UserTokens.Add(expiredToken);
//                model.UserTokens.Add(validToken);
//                return model;
//            });

//            // Act
//            UserbaseSystem.PurgeConnectionTokens();

//            // Assert
//            Assert.Equal(validToken, model.UserTokens.Single());
//        }

//        [Fact]
//        public void PurgeRecoveryTokens()
//        {
//            // Arrange

//            var model = Values.Context;
//            var expiredRecoveryToken = Values.ExpiredRecoveryToken;
//            var validRecoveryToken = Values.ValidRecoveryToken;
//            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
//            {
//                model.UserRecoveryTokens.Add(expiredRecoveryToken);
//                model.UserRecoveryTokens.Add(validRecoveryToken);
//                return model;
//            });

//            // Act
//            UserbaseSystem.PurgeRecoveryTokens();

//            // Assert
//            Assert.Equal(validRecoveryToken, model.UserRecoveryTokens.Single());
//        }

//        [Fact]
//        public void PurgeUsers()
//        {
//            // Arrange

//            var model = Values.Context;
//            var activeUser = Values.UserSpongeBob;
//            var inactiveUser = Values.UserDora;
//            inactiveUser.Active = false;
//            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
//            {
//                model.Users.Add(activeUser);
//                model.Users.Add(inactiveUser);
//                return model;
//            });

//            // Act
//            UserbaseSystem.PurgeUsers();

//            // Assert
//            Assert.Equal(activeUser, model.Users.Single());
//        }
//    }
//}
