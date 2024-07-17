using Nok.Core.Models;

namespace Nok.Infrastructure.Services;

public interface INextOfKinService
{
    Task<Guid> CreateNextOfKinAsync(Guid accessIdentifierId, Guid memberId, NextOfKinDto nextOfKinDto);
    Task<NextOfKinDto> GetNextOfKinAsync(Guid accessIdentifierId, Guid memberId, Guid nextOfKinId);
    Task<IEnumerable<NextOfKinDto>> GetNextOfKinAsync(Guid accessIdentifierId, Guid memberId);
}
