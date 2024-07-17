using Microsoft.EntityFrameworkCore;
using Nok.Core.Aggregates.Register;
using Nok.Infrastructure.Data;

namespace Nok.Infrastructure.Services;

internal static class AccessIdentifierExtensions
{
    public static async Task<AccessIdentifier> GetAccessIdentifierAsync(this DatabaseContext databaseContext, Guid accessIdentifierId)
    {
        var accessIdentifier = await databaseContext.AccessIdentifiers
            .Include(x => x.Members)
            .ThenInclude(x => x.NextOfKins)
            .SingleOrDefaultAsync(x => x.Id == accessIdentifierId);

        if (accessIdentifier is null)
        {
            throw new InvalidOperationException($"Could not find {nameof(AccessIdentifier)}; {accessIdentifierId}");
        }

        return accessIdentifier!;
    }

    public static Member GetMember(this AccessIdentifier accessIdentifier, Guid memberId)
        => accessIdentifier.Members.FirstOrDefault(x => x.Id == memberId)
             ?? throw new InvalidOperationException($"Could not find {nameof(Member)}; {memberId}");
}
