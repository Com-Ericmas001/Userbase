using System;
using System.Collections.Generic;
using Com.Ericmas001.Userbase.Entities;
using Com.Ericmas001.Userbase.Requests;
using Com.Ericmas001.Userbase.Requests.Models;
using Com.Ericmas001.Userbase.Util;
// ReSharper disable All

namespace Com.Ericmas001.Userbase.Test.Util
{

    public static class Values
    {
        public static UserbaseDbContext Context
        {
            get
            {
                var model = new UserbaseDbContext();
                model.SetUpMocks();
                return model;
            }
        }

        public static string Salt => "Salt&P3pp3R";
        public static string UsernameSpongeBob => "spongeb0b";
        public static string UsernameDora => "dora";
        public static string UsernameTooShort => "Po";
        public static string UsernameInvalidChar => "Bling$Bling";

        public static string CommandDumb => "DumbCommand";
        public static string CommandPurge => "PurgeToken";
        public static string CommandBulbapedia => "ExtractBulbapedia";

        public static string PasswordDumb => "dumbPassword";
        public static string PasswordPurge => "Purge42FTW";
        public static string PasswordBulbapedia => "Bulb42FTW";
        public static string PasswordSpongeBob => "B3stDay3v3r";
        public static string PasswordSpongeBobNewOne => "V3ryB3stDay3v3r";
        public static string PasswordDora => "Vamano$";
        public static string PasswordTooShort => "caca";
        public static string PasswordInvalidChar => "caca et pipi";

        public static string DisplayNameSpongeBob => "Sponge BOB";
        public static string DisplayNameSpongeBobNewOne => "SpongeBob SquarePants";
        public static string DisplayNameDora => "Dora The Explorer";
        public static string DisplayNameTooShort => "Me";

        public static string EmailSpongeBob => "spongebob@bikinibottom.org";
        public static string EmailSpongeBobNewOne => "bob@bikinibottom.org";
        public static string EmailDora => "dora@backpack.org";
        public static string EmailNoArobas => "doraAndbackpack.org";

        public static UserToken ExpiredToken => new UserToken { IdUserToken = 21, Expiration = DateTime.Now.AddMinutes(-1), Token = new Guid()};
        public static UserToken ValidToken => new UserToken { IdUserToken = 84, Expiration = DateTime.Now.AddMinutes(1), Token = new Guid() };

        public static UserRecoveryToken ExpiredRecoveryToken => new UserRecoveryToken { Expiration = DateTime.Now.AddMinutes(-1) };
        public static UserRecoveryToken ValidRecoveryToken => new UserRecoveryToken { Expiration = DateTime.Now.AddMinutes(1) };

        public static CreateUserRequest UnregisteredDora => new CreateUserRequest { Authentication = new AuthenticationInfo { Password = PasswordDora, Email = EmailDora }, Profile = new ProfileInfo { DisplayName = DisplayNameDora }, Username = UsernameDora};
        public static AuthenticationInfo NewCredentialsSpongeBob => new AuthenticationInfo { Email = EmailSpongeBobNewOne, Password = PasswordSpongeBobNewOne };
        public static ProfileInfo NewProfileSpongeBob => new ProfileInfo { DisplayName = DisplayNameSpongeBobNewOne };

        public static User UserSpongeBob => new User { IdUser = 42, Name = UsernameSpongeBob, UserTokens = new List<UserToken>(), UserAuthentication = new UserAuthentication { Password = EncryptPassword(PasswordSpongeBob), RecoveryEmail = EmailSpongeBob }, UserProfile = new UserProfile { DisplayName = DisplayNameSpongeBob } };
        public static User UserDora => new User { IdUser = 84, Name = UsernameDora, UserTokens = new List<UserToken>(), UserAuthentication = new UserAuthentication { Password = EncryptPassword(PasswordDora), RecoveryEmail = EmailDora }, UserProfile = new UserProfile { DisplayName = DisplayNameDora } };
        private static string EncryptPassword(string password)
        {
            return BCrypt.HashPassword($"{Salt}{password}", BCrypt.GenerateSalt());
        }

        public static bool VerifyPassword(string password, User u)
        {
            return BCrypt.CheckPassword($"{Salt}{password}", u.UserAuthentication.Password);
        }
    }
}
