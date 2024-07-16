using Nok.Infrastructure.Data.Models;
using System.Security.Claims;

namespace Nok.Api.Services;

public interface IAccessIdentifierService
{
    AccessIdentifier GetOrAddByClaims(IEnumerable<Claim> claims);
}
