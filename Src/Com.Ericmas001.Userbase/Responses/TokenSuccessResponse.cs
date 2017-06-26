using Com.Ericmas001.Userbase.Responses.Models;

namespace Com.Ericmas001.Userbase.Responses
{
    public class TokenSuccessResponse
    {
        public bool Success { get; set; }

        public ConnectionToken Token { get; set; }
    }
}
