using System.Diagnostics.CodeAnalysis;
using Com.Ericmas001.Userbase.DbTasks.Models;
using Com.Ericmas001.Userbase.Responses.Models;
using Com.Ericmas001.Userbase.Test.Util;
using Xunit;
using Moq;

namespace Com.Ericmas001.Userbase.Test
{
    [Collection("Com.Ericmas001.Userbase.Test")]
    public class SendRecoveryTokenTest
    {
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        [SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
        public class DummyEmailSender : IEmailSender
        {
            public virtual void SendToken(RecoveryToken token, string email)
            {
                //Do nothing, 'cause you're dummy !
            }
        }

        [Fact]
        public void EmptyUsersReturnsFalse()
        {
            // Arrange
            UserbaseSystem.Init(Values.Salt, contextGenerator: () => Values.Context);

            // Act
            var result = UserbaseSystem.SendRecoveryToken(Values.UsernameSpongeBob,new DummyEmailSender());

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void ValidUsernameReturnsTrue()
        {
            // Arrange
            var mockDummyEmailSender = new Mock<DummyEmailSender>();
            var user = Values.UserSpongeBob;
            UserbaseSystem.Init(Values.Salt, contextGenerator: delegate
            {
                var model = Values.Context;
                model.Users.Add(user);
                return model;
            });

            // Act
            var result = UserbaseSystem.SendRecoveryToken(Values.UsernameSpongeBob, mockDummyEmailSender.Object);

            // Assert
            Assert.True(result);
            Assert.Equal(1, user.UserRecoveryTokens.Count);
            mockDummyEmailSender.Verify(m => m.SendToken(It.IsAny<RecoveryToken>(), It.IsAny<string>()), Times.Once);
        }
    }
}
