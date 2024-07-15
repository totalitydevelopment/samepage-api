
namespace Nok.Infrastructure.Data.Models;

public static class MemberExtensions
{
    public static bool HasImage(this IMember member) => !string.IsNullOrWhiteSpace(member.ImageUrl);
}
