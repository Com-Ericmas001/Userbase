namespace Com.Ericmas001.Userbase.Models.ServiceInterfaces
{
    public interface IValidationService
    {
        bool ValidateDisplayName(string displayName);
        bool ValidateEmail(string email);
        bool ValidatePassword(string password);
        bool ValidateUsername(string username);
    }
}
