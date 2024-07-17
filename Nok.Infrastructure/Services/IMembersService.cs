using Nok.Core.Models;

namespace Nok.Infrastructure.Services;

public interface IMembersService
{
    Task<Guid> CreateMemberAsync(Guid accessIdentifierId, MemberDto memberDto);
    Task<IEnumerable<MemberDto>> GetMembersAsync(Guid accessIdentifierId, string? searchTerm);
    Task<MemberDto> GetMemberAsync(Guid accessIdentifierId, Guid memberId);
    Task ModifyMemberAsync(Guid accessIdentifierId, Guid memberId, MemberDto memberDto);
}
