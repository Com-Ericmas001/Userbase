using System;
using Com.Ericmas001.Userbase.Test.Util;
using Xunit;

namespace Com.Ericmas001.Userbase.Test
{
    [Collection("Com.Ericmas001.Userbase.Test")]
    public class DeactivateUserTest
    {
        [Fact]
        public void WithNoUserReturnsFalse()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator:() => Values.Context );

            // Act
            var result = UserbaseSystem.Deactivate(Values.UsernameSpongeBob, Values.ValidToken.Token);

            // Assert
            Assert.False(result);
        }

        [Fact]
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
            var result = UserbaseSystem.Deactivate(Values.UsernameDora, Values.ValidToken.Token);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void WithValidUserNoTokensReturnsFalse()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                model.Users.Add(Values.UserSpongeBob);
                return model;
            });

            // Act
            var result = UserbaseSystem.Deactivate(Values.UsernameSpongeBob, Values.ValidToken.Token);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void WithValidUserInvalidTokenReturnsFalse()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                var u = Values.UserSpongeBob;
                u.UserTokens.Add(Values.ValidToken);
                model.Users.Add(u);
                return model;
            });

            // Act
            var result = UserbaseSystem.Deactivate(Values.UsernameSpongeBob, Guid.NewGuid());

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void WithValidUserExpiredTokenReturnsFalse()
        {
            // Arrange
            var tok = Values.ExpiredToken;
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                var u = Values.UserSpongeBob;
                u.UserTokens.Add(tok);
                model.Users.Add(u);
                return model;
            });

            // Act
            var result = UserbaseSystem.Deactivate(Values.UsernameSpongeBob, tok.Token);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void WithValidUserValidTokenReturnsTrue()
        {
            // Arrange
            var tok = Values.ValidToken;
            var u = Values.UserSpongeBob;
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                u.UserTokens.Add(tok);
                model.Users.Add(u);
                return model;
            });

            // Act
            var result = UserbaseSystem.Deactivate(Values.UsernameSpongeBob, tok.Token);

            // Assert
            Assert.True(result);
            Assert.False(u.Active);
        }
    }
}
