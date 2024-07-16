using Nok.Api.Controllers;
using Nok.Core.Extensions;
using Nok.Infrastructure.Data;
using Nok.Infrastructure.Data.Models;
using System.Security.Claims;

namespace Nok.Api.Services;

internal class AccessIdentifierService : IAccessIdentifierService
{
    private readonly ILogger<MembersController> _logger;
    private readonly DatabaseContext _databaseContext;

    public AccessIdentifierService(ILogger<MembersController> logger, DatabaseContext databaseContext)
    {
        _logger = logger;
        _databaseContext = databaseContext;
    }

    public AccessIdentifier GetOrAddByClaims(IEnumerable<Claim> claims)
    {
        var azureOid = claims.GetAzureOid();
        var identifier = _databaseContext.AccessIdentifiers.FirstOrDefault(x => x.AzureOid == azureOid);

        if (identifier is null)
        {
            identifier = new AccessIdentifier()
            {
                AzureOid = azureOid,
                Id = Guid.NewGuid(),
                Type = claims.GetAccessIdentifierType()
            };

            _databaseContext.Add(identifier);
            _databaseContext.SaveChanges();
        }

        return identifier;
    }
}
