namespace Tielvh.Emby.Notification.Ntfy.Test.ClassEnum;

public class ClassEnumTest
{
    [Test]
    public void GivenEnumExists_WhenGettingByName_EnumIsReturned()
    {
        var testEnum = TestEnum.One;

        var enumByName = TestEnum.FromName("One");

        Assert.That(enumByName, Is.EqualTo(testEnum));
    }
}