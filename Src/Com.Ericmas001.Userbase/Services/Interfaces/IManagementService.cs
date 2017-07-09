using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Ericmas001.Userbase.Models.Responses;

namespace Com.Ericmas001.Userbase.Services.Interfaces
{
    public interface IManagementService
    {
        void PurgeUsers();
        void PurgeConnectionTokens();
        void PurgeRecoveryTokens();
    }
}
