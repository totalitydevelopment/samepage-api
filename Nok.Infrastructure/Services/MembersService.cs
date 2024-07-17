using AutoMapper;
using Nok.Core.Aggregates.Register;
using Nok.Core.Models;
using Nok.Infrastructure.Data;

namespace Nok.Infrastructure.Services;

public class MembersService : IMembersService
{
    private readonly DatabaseContext _databaseContext;
    private readonly IMapper _mapper;

    public MembersService(DatabaseContext databaseContext, IMapper mapper)
    {
        _databaseContext = databaseContext;
        _mapper = mapper;
    }

    public async Task<Guid> CreateMemberAsync(Guid accessIdentifierId, MemberDto memberDto)
    {
        var accessIdentifier = await _databaseContext.GetAccessIdentifierAsync(accessIdentifierId);

        var member = _mapper.Map<Member>(memberDto);
        accessIdentifier.Members.Add(member);

        await _databaseContext.SaveChangesAsync();

        return member.Id;
    }

    public async Task<MemberDto> GetMemberAsync(Guid accessIdentifierId, Guid memberId)
    {
        var accessIdentifier = await _databaseContext.GetAccessIdentifierAsync(accessIdentifierId);
        var member = accessIdentifier.GetMember(memberId);

        return _mapper.Map<MemberDto>(member);
        // TODO do something with the images URL. That something probably doesn't belong here.
        // ImageUrl = member.HasImage ? "https://noktemp.blob.core.windows.net/images/" + member.ImageUrl : string.Empty
    }

    public async Task<IEnumerable<MemberDto>> GetMembersAsync(Guid accessIdentifierId, string? searchTerm)
    {
        var accessIdentifier = await _databaseContext.GetAccessIdentifierAsync(accessIdentifierId);

        IEnumerable<Member> members = accessIdentifier.Members;

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            members = members.Where(u => u.Name.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) || u.Name.Surname.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        return _mapper.Map<IEnumerable<MemberDto>>(members);
    }

    public Task ModifyMemberAsync(Guid accessIdentifierId, Guid memberId, MemberDto memberDto)
    {
        throw new NotImplementedException();
    }
}
