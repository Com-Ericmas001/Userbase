using System.Linq;
using Com.Ericmas001.Userbase.Test.Util;
using Xunit;

namespace Com.Ericmas001.Userbase.Test
{
    [Collection("Com.Ericmas001.Userbase.Test")]
    public class ManagementTest
    {

        [Fact]
        public void PurgeConnectionTokens()
        {
            // Arrange
            var expiredToken = Values.ExpiredToken;
            var validToken = Values.ValidToken;
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                model.UserTokens.Add(expiredToken);
                model.UserTokens.Add(validToken);
            });

            // Act
            util.System.PurgeConnectionTokens();

            // Assert
            Assert.Equal(validToken, util.Model.UserTokens.Single());
        }

        [Fact]
        public void PurgeRecoveryTokens()
        {
            // Arrange
            var expiredRecoveryToken = Values.ExpiredRecoveryToken;
            var validRecoveryToken = Values.ValidRecoveryToken;
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                model.UserRecoveryTokens.Add(expiredRecoveryToken);
                model.UserRecoveryTokens.Add(validRecoveryToken);
            });

            // Act
            util.System.PurgeRecoveryTokens();

            // Assert
            Assert.Equal(validRecoveryToken, util.Model.UserRecoveryTokens.Single());
        }

        [Fact]
        public void PurgeUsers()
        {
            // Arrange
            var activeUser = Values.UserSpongeBob;
            var inactiveUser = Values.UserDora;
            inactiveUser.Active = false;
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                model.Users.Add(activeUser);
                model.Users.Add(inactiveUser);
            });

            // Act
            util.System.PurgeUsers();

            // Assert
            Assert.Equal(activeUser, util.Model.Users.Single());
        }
    }
}
