using Tielvh.Emby.Notification.Ntfy.Util;

namespace Tielvh.Emby.Notification.Ntfy.QuotedPrintable
{
    public sealed class Encoding : ClassEnum<Encoding, string>
    {
        public static readonly Encoding B64 = new(nameof(B64), "B");

        private Encoding(string name, string value) : base(name, value)
        {
        }
    }
}