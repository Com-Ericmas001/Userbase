//using System;
//using Com.Ericmas001.Userbase.Test.Util;
//using Xunit;

//namespace Com.Ericmas001.Userbase.Test
//{
//    [Collection("Com.Ericmas001.Userbase.Test")]
//    public class ValidateTokenTest
//    {
//        [Fact]
//        public void WithNoUserReturnsFalse()
//        {
//            // Arrange
//            UserbaseSystem.Init(Values.Salt, contextGenerator:() => Values.Context );

//            // Act
//            var result = UserbaseSystem.ValidateToken(Values.UsernameSpongeBob, Values.ValidToken.Token);

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
//            var result = UserbaseSystem.ValidateToken(Values.UsernameDora, Values.ValidToken.Token);

//            // Assert
//            Assert.False(result.Success);
//        }
//        [Fact]
//        public void WithValidUserNoTokensReturnsFalse()
//        {
//            // Arrange
//            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
//            {
//                var model = Values.Context;
//                model.Users.Add(Values.UserSpongeBob);
//                return model;
//            });

//            // Act
//            var result = UserbaseSystem.ValidateToken(Values.UsernameSpongeBob, Values.ValidToken.Token);

//            // Assert
//            Assert.False(result.Success);
//        }
//        [Fact]
//        public void WithValidUserInvalidTokenReturnsFalse()
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
//            var result = UserbaseSystem.ValidateToken(Values.UsernameSpongeBob, Guid.NewGuid());

//            // Assert
//            Assert.False(result.Success);
//        }
//        [Fact]
//        public void WithValidUserExpiredTokenReturnsFalse()
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
//            var result = UserbaseSystem.ValidateToken(Values.UsernameSpongeBob, tok.Token);

//            // Assert
//            Assert.False(result.Success);
//        }
//        [Fact]
//        public void WithValidUserValidTokenReturnsTrue()
//        {
//            // Arrange
//            var tok = Values.ValidToken;
//            var expiration = tok.Expiration;
//            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
//            {
//                var model = Values.Context;
//                var u = Values.UserSpongeBob;
//                u.UserTokens.Add(tok);
//                model.Users.Add(u);
//                return model;
//            });

//            // Act
//            var result = UserbaseSystem.ValidateToken(Values.UsernameSpongeBob, tok.Token);

//            // Assert
//            Assert.True(result.Success);
//            Assert.Equal(tok.Token, result.Token.Id);
//            Assert.True(expiration < result.Token.ValidUntil);
//        }
//    }
//}
