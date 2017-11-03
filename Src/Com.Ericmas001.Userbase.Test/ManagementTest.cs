using System.Linq;
using Com.Ericmas001.Userbase.Models.ServiceInterfaces;
using Com.Ericmas001.Userbase.Test.Util;
using FluentAssertions;
using Unity;
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
            util.Container.Resolve<IManagementService>().PurgeConnectionTokens();

            // Assert
            util.Model.UserTokens.Single().Should().Be(validToken);
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
            util.Container.Resolve<IManagementService>().PurgeRecoveryTokens();

            // Assert
            util.Model.UserRecoveryTokens.Single().Should().Be(validRecoveryToken);
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
            util.Container.Resolve<IManagementService>().PurgeUsers();

            // Assert
            util.Model.Users.Single().Should().Be(activeUser);
        }
    }
}
