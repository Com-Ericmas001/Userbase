using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Ericmas001.Userbase.Models.Responses;

namespace Com.Ericmas001.Userbase.Services.Interfaces
{
    public interface IValidationService
    {
        bool ValidateDisplayName(string displayName);
        bool ValidateEmail(string email);
        bool ValidatePassword(string password);
        bool ValidateUsername(string username);
    }
}
