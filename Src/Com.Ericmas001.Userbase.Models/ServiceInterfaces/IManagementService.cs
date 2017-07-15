namespace Com.Ericmas001.Userbase.Models.ServiceInterfaces
{
    public interface IManagementService
    {
        void PurgeUsers();
        void PurgeConnectionTokens();
        void PurgeRecoveryTokens();
    }
}
