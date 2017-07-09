using System;
using System.Linq;
using Com.Ericmas001.Userbase.Services.Interfaces;

namespace Com.Ericmas001.Userbase.Services
{
    public class ValidationService : IValidationService
    {
        public bool ValidateDisplayName(string displayName)
        {
            if (displayName.Length < 3)
                return false;

            return true;
        }

        public bool ValidateEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool ValidatePassword(string password)
        {
            if (password.Length < 6)
                return false;

            if (!password.All(c => Char.IsLetterOrDigit(c) || Char.IsSymbol(c)))
                return false;

            return true;
        }

        public bool ValidateUsername(string username)
        {
            if (username.Length < 3)
                return false;

            if (!username.All(Char.IsLetterOrDigit))
                return false;

            return true;
        }
    }
}
