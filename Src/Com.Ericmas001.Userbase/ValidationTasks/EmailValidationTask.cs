namespace Com.Ericmas001.Userbase.ValidationTasks
{
    public class EmailValidationTask
    {
        public bool Validate(string email)
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
    }
}
