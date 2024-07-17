using System.Security.Claims;

namespace Nok.Infrastructure.Services;

public interface IAccessIdentifierService
{
    Task<Guid> GetOrCreateByClaimsAsync(IEnumerable<Claim> claims);
}
