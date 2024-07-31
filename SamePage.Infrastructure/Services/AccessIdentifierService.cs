using Microsoft.EntityFrameworkCore;
using SamePage.Core.Aggregates.Register;
using SamePage.Infrastructure.Data;
using System.Security.Claims;

namespace SamePage.Infrastructure.Services;

public class AccessIdentifierService : IAccessIdentifierService
{
    private readonly DatabaseContext _databaseContext;

    public AccessIdentifierService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<Guid> GetOrCreateByClaimsAsync(IEnumerable<Claim> claims)
    {
        var azureOid = claims.GetAzureOid();
        //var identifier = await _databaseContext.AccessIdentifiers.FirstOrDefaultAsync(x => x.AzureOid == azureOid);
        var identifier = await _databaseContext.AccessIdentifiers
            .SingleOrDefaultAsync(x => x.AzureOid == azureOid);

        if (identifier is null)
        {
            identifier = new AccessIdentifier(Guid.NewGuid(), azureOid, claims.GetAccessIdentifierType());

            await _databaseContext.AddAsync(identifier);
            await _databaseContext.SaveChangesAsync();
        }

        return identifier.Id;
    }
}
