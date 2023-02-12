using Microsoft.AspNetCore.Authorization;
using UserAuthenticationAPI.Model;

namespace UserAuthenticationAPI.Authorization {
    public class PermissionRequirement : IAuthorizationRequirement {
        public PermissionRequirement(string accessLevel) {
            AccessLevel = (AccessLevel)Enum.Parse(typeof(AccessLevel), accessLevel);
        }

        public AccessLevel AccessLevel { get; set; }
    }
}
