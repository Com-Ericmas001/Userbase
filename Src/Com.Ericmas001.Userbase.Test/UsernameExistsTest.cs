﻿using Com.Ericmas001.Userbase.Test.Util;
using Xunit;

namespace Com.Ericmas001.Userbase.Test
{
    public class UsernameExistsTest
    {
        [Fact]
        public void WithNoUserReturnsFalse()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate { });

            // Act
            var result = util.System.UsernameExists(Values.UsernameSpongeBob);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void WithInvalidUserReturnsFalse()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                model.Users.Add(Values.UserSpongeBob);
            });

            // Act
            var result = util.System.UsernameExists(Values.UsernameDora);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void WithValidUserReturnsTrue()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                model.Users.Add(Values.UserSpongeBob);
            });


            // Act
            var result = util.System.UsernameExists(Values.UsernameSpongeBob);

            // Assert
            Assert.True(result);
        }
    }
}
