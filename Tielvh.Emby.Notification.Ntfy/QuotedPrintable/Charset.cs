using Tielvh.Emby.Notification.Ntfy.Util;

namespace Tielvh.Emby.Notification.Ntfy.QuotedPrintable
{
    public sealed class Charset : ClassEnum<Charset, string>
    {
        public static readonly Charset Utf8 = new(nameof(Utf8), "UTF-8");

        private Charset(string name, string value) : base(name, value)
        {
        }
    }
}