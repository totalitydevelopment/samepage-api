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

    public async Task<Guid> CreateNextOfKinAsync(Guid accessIdentifierId, Guid memberId, NextOfKinDto nextOfKinDto)
    {
        var accessIdentifier = await _databaseContext.GetAccessIdentifierAsync(accessIdentifierId);
        var member = accessIdentifier.GetMember(memberId);

        var nextOfKin = _mapper.Map<NextOfKin>(nextOfKinDto);
        member.NextOfKins.Add(nextOfKin);
        await _databaseContext.SaveChangesAsync();

        return nextOfKin.Id;
    }

    public async Task<NextOfKinDto> GetNextOfKinAsync(Guid accessIdentifierId, Guid memberId, Guid nextOfKinId)
    {
        var accessIdentifier = await _databaseContext.GetAccessIdentifierAsync(accessIdentifierId);
        var member = accessIdentifier.GetMember(memberId);

        var nextOfKin = member.NextOfKins.FirstOrDefault(x => x.Id == nextOfKinId)
            ?? throw new InvalidOperationException($"Could not find {nameof(NextOfKin)}; {nextOfKinId}");

        return _mapper.Map<NextOfKinDto>(nextOfKin);
    }

    public async Task<IEnumerable<NextOfKinDto>> GetNextOfKinAsync(Guid accessIdentifierId, Guid memberId)
    {
        var accessIdentifier = await _databaseContext.GetAccessIdentifierAsync(accessIdentifierId);
        var member = accessIdentifier.GetMember(memberId);

        return _mapper.Map<IEnumerable<NextOfKinDto>>(member.NextOfKins);
    }
}
