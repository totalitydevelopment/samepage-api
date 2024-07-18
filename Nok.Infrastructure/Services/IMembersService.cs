using Nok.Core.Models;

namespace Nok.Infrastructure.Services;

public interface IMembersService
{
    Task<Guid> CreateMemberAsync(Guid accessIdentifierId, MemberRequest memberDto);
    Task<IEnumerable<MemberResponse>> GetMembersAsync(Guid accessIdentifierId, string? searchTerm);
    Task<MemberResponse> GetMemberAsync(Guid accessIdentifierId, Guid memberId);
    Task ModifyMemberAsync(Guid accessIdentifierId, Guid memberId, MemberRequest memberDto);
}
