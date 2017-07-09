//using System;
//using System.Linq;
//using Com.Ericmas001.Userbase.Test.Util;
//using Xunit;

//namespace Com.Ericmas001.Userbase.Test
//{
//    [Collection("Com.Ericmas001.Userbase.Test")]
//    public class UserSummaryTest
//    {
//        [Fact]
//        public void WithInvalidUsernameReturnsFalse()
//        {
//            // Arrange
//            UserbaseSystem.Init(Values.Salt, contextGenerator: () => Values.Context);

//            // Act
//            var result = UserbaseSystem.UserSummary(Values.UsernameSpongeBob, Guid.NewGuid(), Values.UsernameDora);

//            // Assert
//            Assert.Null(result.Token);
//            Assert.False(result.Success);
//        }
//        [Fact]
//        public void WithValidUsernameButNoTokenReturnsFalse()
//        {
//            // Arrange
//            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
//            {
//                var model = Values.Context;
//                model.Users.Add(Values.UserSpongeBob);
//                return model;
//            });

//            // Act
//            var result = UserbaseSystem.UserSummary(Values.UsernameSpongeBob, Guid.NewGuid(), Values.UsernameDora);

//            // Assert
//            Assert.Null(result.Token);
//            Assert.False(result.Success);
//        }
//        [Fact]
//        public void WithValidUsernameButInvalidTokenReturnsFalse()
//        {
//            // Arrange
//            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
//            {
//                var model = Values.Context;
//                var u = Values.UserSpongeBob;
//                u.UserTokens.Add(Values.ValidToken);
//                model.Users.Add(u);
//                return model;
//            });

//            // Act
//            var result = UserbaseSystem.UserSummary(Values.UsernameSpongeBob, Guid.NewGuid(), Values.UsernameDora);

//            // Assert
//            Assert.Null(result.Token);
//            Assert.False(result.Success);
//        }
//        [Fact]
//        public void WithValidUsernameButExpiredTokenReturnsFalse()
//        {
//            // Arrange
//            var tok = Values.ExpiredToken;
//            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
//            {
//                var model = Values.Context;
//                var u = Values.UserSpongeBob;
//                u.UserTokens.Add(tok);
//                model.Users.Add(u);
//                return model;
//            });

//            // Act
//            var result = UserbaseSystem.UserSummary(Values.UsernameSpongeBob, tok.Token, Values.UsernameDora);

//            // Assert
//            Assert.Null(result.Token);
//            Assert.False(result.Success);
//        }
//        [Fact]
//        public void WithValidUsernameValidNotExpiredTokenButInvalidRequestedUsernameReturnsFalse()
//        {
//            // Arrange
//            var tok = Values.ValidToken;
//            var u = Values.UserSpongeBob;
//            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
//            {
//                var model = Values.Context;
//                u.UserTokens.Add(tok);
//                model.Users.Add(u);
//                return model;
//            });

//            // Act
//            var result = UserbaseSystem.UserSummary(Values.UsernameSpongeBob, tok.Token, Values.UsernameDora);

//            // Assert
//            Assert.Null(result.Token);
//            Assert.False(result.Success);
//        }
//        [Fact]
//        public void WithValidUsernameValidNotExpiredToken()
//        {
//            // Arrange
//            var tok = Values.ValidToken;
//            var originalTime = tok.Expiration;
//            var u = Values.UserSpongeBob;
//            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
//            {
//                var model = Values.Context;
//                u.UserTokens.Add(tok);
//                model.Users.Add(u);
//                model.Users.Add(Values.UserDora);
//                return model;
//            });

//            // Act
//            var result = UserbaseSystem.UserSummary(Values.UsernameSpongeBob, tok.Token, Values.UsernameDora);

//            // Assert
//            Assert.NotNull(result.Token);
//            Assert.True(tok.Expiration > originalTime);
//            Assert.Equal(Values.DisplayNameDora, result.DisplayName);
//            Assert.Equal(1, result.Groups.Count);
//            Assert.Equal(Values.GroupDummyId, result.Groups.First().Key);
//            Assert.Equal(Values.GroupDummyName, result.Groups.First().Value);
//        }
//    }
//}
