using System.Security.Claims;

namespace SamePage.Infrastructure.Services;

public interface IAccessIdentifierService
{
    Task<Guid> GetOrCreateByClaimsAsync(IEnumerable<Claim> claims);
}
