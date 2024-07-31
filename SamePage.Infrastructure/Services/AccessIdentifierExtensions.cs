using Microsoft.EntityFrameworkCore;
using SamePage.Core.Aggregates.Register;
using SamePage.Core.Enums;
using SamePage.Infrastructure.Data;

namespace SamePage.Infrastructure.Services;

internal static class AccessIdentifierExtensions
{
    public static async Task<AccessIdentifier> GetAccessIdentifierAsync(this DatabaseContext databaseContext, Guid accessIdentifierId)
    {
        var accessIdentifier = await databaseContext.AccessIdentifiers
            .Include(x => x.Members)
            .ThenInclude(x => x.NextOfKin)
            .SingleOrDefaultAsync(x => x.Id == accessIdentifierId);

        if (accessIdentifier is null)
        {
            throw new InvalidOperationException($"Could not find {nameof(AccessIdentifier)}; {accessIdentifierId}");
        }

        return accessIdentifier!;
    }

    public static async Task<Member?> GetMember(this AccessIdentifier accessIdentifier, DatabaseContext databaseContext, Guid memberId) =>
        accessIdentifier.Type is AccessIdentifierType.Api
            ? await databaseContext.Members
                .Include(x => x.NextOfKin)
                .FirstOrDefaultAsync(x => x.Id == memberId)
            : accessIdentifier.Members
                .FirstOrDefault(x => x.Id == memberId);
}
