using Microsoft.AspNetCore.Authorization;

namespace PosApiJwt.Requirements
{
    public class UserBlockedStatusRequirement : IAuthorizationRequirement
    {
        public bool IsBlocked { get; }
        public UserBlockedStatusRequirement(bool isBlocked)
        {
            IsBlocked = isBlocked;
        }

    }
}
