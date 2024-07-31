namespace SamePage.Core.Extensions;

public static class SystemTime
{
    public static Func<DateTime> Now = () => DateTime.Now;
    public static Func<DateTime> UtcNow = () => DateTime.UtcNow;
}
