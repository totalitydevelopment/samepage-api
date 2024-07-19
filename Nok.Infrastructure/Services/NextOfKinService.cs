using AutoMapper;
using Nok.Core.Aggregates.Register;
using Nok.Core.Models;
using Nok.Infrastructure.Data;

namespace Nok.Infrastructure.Services;

public class NextOfKinService : INextOfKinService
{
    private readonly DatabaseContext _databaseContext;
    private readonly IMapper _mapper;

    public NextOfKinService(DatabaseContext databaseContext, IMapper mapper)
    {
        _databaseContext = databaseContext;
        _mapper = mapper;
    }

    public async Task<Guid?> CreateNextOfKinAsync(Guid accessIdentifierId, Guid memberId, NextOfKinRequest nextOfKinRequest)
    {
        var accessIdentifier = await _databaseContext.GetAccessIdentifierAsync(accessIdentifierId);
        var member = await accessIdentifier.GetMember(_databaseContext, memberId);

        if (member is null)
        {
            return null;
        }

        var nextOfKinRequestWithId = new NextOfKinRequestWithId(nextOfKinRequest)
        {
            Id = Guid.NewGuid(),
        };

        var nextOfKin = _mapper.Map<NextOfKin>(nextOfKinRequestWithId);
        member.NextOfKin.Add(nextOfKin);
        await _databaseContext.SaveChangesAsync();

        return nextOfKin.Id;
    }

    public async Task<NextOfKinResponse?> GetNextOfKinAsync(Guid accessIdentifierId, Guid memberId, Guid nextOfKinId)
    {
        var accessIdentifier = await _databaseContext.GetAccessIdentifierAsync(accessIdentifierId);
        var member = await accessIdentifier.GetMember(_databaseContext, memberId);

        if (member is null)
        {
            return null;
        }

        var nextOfKin = member.NextOfKin.FirstOrDefault(x => x.Id == nextOfKinId);

        if (nextOfKin is null)
        {
            return null;
        }

        return _mapper.Map<NextOfKinResponse>(nextOfKin);
    }

    public async Task<IEnumerable<NextOfKinResponse>?> GetNextOfKinAsync(Guid accessIdentifierId, Guid memberId)
    {
        var accessIdentifier = await _databaseContext.GetAccessIdentifierAsync(accessIdentifierId);
        var member = await accessIdentifier.GetMember(_databaseContext, memberId);

        if (member is null)
        {
            return null;
        }

        return _mapper.Map<IEnumerable<NextOfKinResponse>>(member.NextOfKin);
    }
}
