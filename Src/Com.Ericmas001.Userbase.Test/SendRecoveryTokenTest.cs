using System.Diagnostics.CodeAnalysis;
using Com.Ericmas001.Userbase.Models;
using Com.Ericmas001.Userbase.Test.Util;
using Xunit;
using Moq;

namespace Com.Ericmas001.Userbase.Test
{
    public class SendRecoveryTokenTest
    {
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        [SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
        public class DummyEmailSender : IEmailSender
        {
            public virtual void SendToken(RecoveryToken token, string username, string email)
            {
                //Do nothing, 'cause you're dummy !
            }
        }

        [Fact]
        public void EmptyUsersReturnsFalse()
        {
            // Arrange
            var util = new UserbaseSystemUtil(delegate { });

            // Act
            var result = util.System.SendRecoveryToken(Values.UsernameSpongeBob, new DummyEmailSender());

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void ValidUsernameReturnsTrue()
        {
            // Arrange
            var mockDummyEmailSender = new Mock<DummyEmailSender>();
            var user = Values.UserSpongeBob;
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                model.Users.Add(user);
            });

            // Act
            var result = util.System.SendRecoveryToken(Values.UsernameSpongeBob, mockDummyEmailSender.Object);

            // Assert
            Assert.True(result);
            Assert.Equal(1, user.UserRecoveryTokens.Count);
            mockDummyEmailSender.Verify(m => m.SendToken(It.IsAny<RecoveryToken>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
