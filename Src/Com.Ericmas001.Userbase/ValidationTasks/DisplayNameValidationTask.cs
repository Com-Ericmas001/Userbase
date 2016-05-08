namespace Com.Ericmas001.Userbase.ValidationTasks
{
    public class DisplayNameValidationTask
    {
        public bool Validate(string displayName)
        {
            if (displayName.Length < 3)
                return false;

            return true;
        }
    }
}
