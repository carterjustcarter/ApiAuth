using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using UserAuthenticationAPI.Model;

namespace UserAuthenticationAPI.Authorization {
    public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement> {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement) {
            var claims = ((ClaimsIdentity)context.User.Identity).Claims;
            if (claims.Count() == 0 || claims == null) {
                context.Fail();
                return Task.CompletedTask;
            }
            var accessLevelClaim = claims.ToList().Find(x => x.Type == "AccessLevel").Value;
            if ((AccessLevel)Enum.Parse(typeof(AccessLevel), accessLevelClaim) >= requirement.AccessLevel) {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            context.Fail();
            return Task.CompletedTask;
        }
    }
}
