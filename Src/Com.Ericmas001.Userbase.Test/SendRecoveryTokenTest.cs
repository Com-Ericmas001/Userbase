using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Com.Ericmas001.Userbase.Models;
using Com.Ericmas001.Userbase.Models.ServiceInterfaces;
using Com.Ericmas001.Userbase.Test.Util;
using FluentAssertions;
using Unity;
using Xunit;

namespace Com.Ericmas001.Userbase.Test
{

    [Collection("Com.Ericmas001.Userbase.Test")]
    public class SendRecoveryTokenTest
    {
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        [SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
        public class DummyEmailSender : ISendEmailService
        {
            public List<Tuple<RecoveryToken,string,string>> TokenSent { get; } = new List<Tuple<RecoveryToken, string, string>>();
            public void SendRecoveryToken(RecoveryToken token, string username, string email)
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
            var result = util.Container.Resolve<IUserRecoveryService>().SendRecoveryToken(Values.UsernameSpongeBob);

            // Assert
            result.Should().BeFalse();
        }
        [Fact]
        public void ValidUsernameReturnsTrue()
        {
            // Arrange
            var user = Values.UserSpongeBob;
            var util = new UserbaseSystemUtil(delegate (IUserbaseDbContext model)
            {
                model.Users.Add(user);
            });

            // Act
            var result = util.Container.Resolve<IUserRecoveryService>().SendRecoveryToken(Values.UsernameSpongeBob);

            // Assert
            result.Should().BeTrue();
            user.UserRecoveryTokens.Count.Should().Be(1);
            var token = user.UserRecoveryTokens.Single();

            util.EmailSender.TokenSent.Count.Should().Be(1);
            var sended = util.EmailSender.TokenSent.Single();

            sended.Item1.Id.Should().Be(token.Token);
            sended.Item1.ValidUntil.Should().Be(token.Expiration);
            sended.Item2.Should().Be(user.Name);
            sended.Item3.Should().Be(user.UserAuthentication.RecoveryEmail);
        }
    }
}
