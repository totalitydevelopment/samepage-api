using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Nok.Api;

public static class AuthorizationExtensions
{
    private static readonly IEnumerable<string> _scopeClaimTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "http://schemas.microsoft.com/identity/claims/scope",
        "scope"
    };

    // ref: https://stackoverflow.com/a/57577319
    public static AuthorizationPolicyBuilder RequireScope(this AuthorizationPolicyBuilder builder, params string[] scopes) =>
        builder.RequireAssertion(context =>
            context.User
                .Claims
                .Where(c => _scopeClaimTypes.Contains(c.Type))
                .SelectMany(c => c.Value.Split(' '))
                .Any(s => scopes.Contains(s, StringComparer.Ordinal)));

    public static AuthorizationOptions AddRequireScopePolicy(this AuthorizationOptions options, string policyName, params string[] scopes)
    {
        options.AddPolicy(policyName, policy => policy.RequireScope(scopes));

        return options;
    }
}
