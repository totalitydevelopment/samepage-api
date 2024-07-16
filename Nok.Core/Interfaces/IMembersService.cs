using Nok.Core.Models;

namespace Nok.Core.Interfaces;

public interface IMembersService
{
    Task<Guid> CreateMemberAsync(CreateMember newMember);
    Task<GetMember?> GetMemberAsync(Guid id);
    Task<IEnumerable<GetMemberListItem>> GetListAsync(string? searchTerm = null);
}
