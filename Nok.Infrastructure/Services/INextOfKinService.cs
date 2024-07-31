using SamePage.Core.Models;

namespace SamePage.Infrastructure.Services;

public interface INextOfKinService
{
    Task<Guid?> CreateNextOfKinAsync(Guid accessIdentifierId, Guid memberId, NextOfKinRequest nextOfKinDto);
    Task<NextOfKinResponse?> GetNextOfKinAsync(Guid accessIdentifierId, Guid memberId, Guid nextOfKinId);
    Task<IEnumerable<NextOfKinResponse>?> GetNextOfKinAsync(Guid accessIdentifierId, Guid memberId);
}
