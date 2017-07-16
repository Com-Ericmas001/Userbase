using System.Collections.Generic;

namespace Com.Ericmas001.Userbase.Models.Responses
{
    public class UserSummaryResponse : TokenSuccessResponse
    {
        public string DisplayName { get; set; }
        public IEnumerable<GroupInfo> Groups { get; set; }
    }
}
