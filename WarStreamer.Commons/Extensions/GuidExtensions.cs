using System.Security.Cryptography;
using System.Text;

namespace WarStreamer.Commons.Extensions
{
    public static class GuidExtensions
    {
        /* * * * * * * * * * * * * * * * * *\
        |*               GUID              *|
        \* * * * * * * * * * * * * * * * * */

        public static Guid ParseDiscordId(this Guid _, string id)
        {
            byte[] idBytes = Encoding.UTF8.GetBytes(id);
            byte[] hashBytes = SHA256.HashData(idBytes);

            return new Guid(hashBytes.Take(16).ToArray());
        }
    }
}
