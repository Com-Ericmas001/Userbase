using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Com.Ericmas001.Userbase.Models;
using Com.Ericmas001.Userbase.Test.Util;
using Xunit;

namespace Com.Ericmas001.Userbase.Test
{
    public class SendRecoveryTokenTest
    {
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        [SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
        public class DummyEmailSender : IEmailSender
        {
            public List<Tuple<RecoveryToken,string,string>> TokenSent { get; } = new List<Tuple<RecoveryToken, string, string>>();
            public virtual void SendToken(RecoveryToken token, string username, string email)
            {
                TokenSent.Add(new Tuple<RecoveryToken, string, string>(token,username,email));
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
            var emailSender = new DummyEmailSender();
            var user = Values.UserSpongeBob;
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                model.Users.Add(user);
            });

            // Act
            var result = util.System.SendRecoveryToken(Values.UsernameSpongeBob, emailSender);

            // Assert
            Assert.True(result);
            Assert.Equal(1, user.UserRecoveryTokens.Count);
            var token = user.UserRecoveryTokens.Single();

            Assert.Equal(1, emailSender.TokenSent.Count);
            var sended = emailSender.TokenSent.Single();

            Assert.Equal(token.Token, sended.Item1.Id);
            Assert.Equal(token.Expiration, sended.Item1.ValidUntil);
            Assert.Equal(user.Name, sended.Item2);
            Assert.Equal(user.UserAuthentication.RecoveryEmail, sended.Item3);
        }
    }
}
