using System;
using System.Text;

namespace Tielvh.Emby.Notification.Ntfy.QuotedPrintable
{
    public class QuotedPrintableEncoder
    {
        public Charset Charset { get; set; } = Charset.Utf8;
        public Encoding Encoding { get; set; } = Encoding.B64;

        public string Encode(string value)
        {
            var sb = new StringBuilder();

            sb.Append("=?");
            sb.Append(Charset.ToString());

            sb.Append("?");
            sb.Append(Encoding.Name);

            sb.Append("?");
            var encodedValue = EncodeValue(value);
            sb.Append(encodedValue);

            sb.Append("?=");

            return sb.ToString();
        }

        private string EncodeValue(string value)
        {
            if (Encoding == Encoding.B64)
            {
                byte[] bytes;
                if (Charset == Charset.Utf8)
                {
                    bytes = System.Text.Encoding.UTF8.GetBytes(value);
                }
                else
                {
                    throw new InvalidOperationException("Not configured properly");
                }

                return Convert.ToBase64String(bytes);
            }

            throw new InvalidOperationException("Not configured properly");
        }
    }
}