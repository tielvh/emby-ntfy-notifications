using Randomizer.Types;
using Tielvh.Emby.Notification.Ntfy.QuotedPrintable;

namespace Tielvh.Emby.Notification.Ntfy.Test;

public class QuotedPrintableEncoderTest
{
    private static readonly RandomStringGenerator RandomStringGenerator = new();

    [TestCase("Utf8", "UTF-8")]
    public void CharsetIsAddedCorrectly(string input, string expectedOutput)
    {
        var randomValue = RandomStringGenerator.GenerateValue();
        var charset = Charset.FromName(input);
        var encoder = new QuotedPrintableEncoder { Charset = charset };

        var encodedValue = encoder.Encode(randomValue);

        StringAssert.Contains($"=?{expectedOutput}?", encodedValue);
    }

    [TestCase("B64", "B")]
    public void EncodingIsAddedCorrectly(string input, string expectedOutput)
    {
        var randomValue = RandomStringGenerator.GenerateValue();
        var encoding = Encoding.FromName(input);
        var encoder = new QuotedPrintableEncoder { Encoding = encoding };

        var encodedValue = encoder.Encode(randomValue);

        StringAssert.Contains($"?{expectedOutput}?", encodedValue);
    }
}