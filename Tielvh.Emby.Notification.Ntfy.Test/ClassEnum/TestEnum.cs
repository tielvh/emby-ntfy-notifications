using Tielvh.Emby.Notification.Ntfy.Util;

namespace Tielvh.Emby.Notification.Ntfy.Test.ClassEnum;

public sealed class TestEnum: ClassEnum<TestEnum, int>
{
    public static readonly TestEnum One = new(nameof(One), 1);
    
    private TestEnum(string name, int value) : base(name, value)
    {
    }
}