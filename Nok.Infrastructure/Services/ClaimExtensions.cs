using Nok.Core.Enums;
using System.Security.Claims;
using System.Security.Principal;

namespace Nok.Infrastructure.Services;

public static class ClaimExtensions
{
    private const string OidValueTypeString = "http://schemas.microsoft.com/identity/claims/objectidentifier";
    private const string ScopeValueTypeString = "http://schemas.microsoft.com/identity/claims/scope";

    public static IEnumerable<Claim> GetClaims(this IIdentity securityPrincipleIdentity) =>
        ((ClaimsIdentity)securityPrincipleIdentity).Claims;

    public static Guid GetAzureOid(this IEnumerable<Claim> claims) =>
        Guid.Parse(claims.Single(x => x.Type is OidValueTypeString).Value);

    public static AccessIdentifierType GetAccessIdentifierType(this IEnumerable<Claim> claims) =>
        claims.First(x => x.Type is ScopeValueTypeString).Value.StartsWith("app.", StringComparison.InvariantCultureIgnoreCase)
            ? AccessIdentifierType.Api
            : AccessIdentifierType.User;
}
