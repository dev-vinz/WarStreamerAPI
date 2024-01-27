using System.ComponentModel.DataAnnotations;
using WarStreamer.Models.EntityBase;

namespace WarStreamer.Models
{
    public class AuthToken : Entity
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [Key]
        public Guid UserId { get; private set; }

        public string AccessToken { get; set; } = null!;

        public string AccessIV { get; set; } = null!;

        public string DiscordToken { get; set; } = null!;

        public string DiscordIV { get; set; } = null!;

        public DateTimeOffset IssuedAt { get; private set; }

        public DateTimeOffset ExpiresAt { get; private set; }

        /* * * * * * * * * * * * * * * * * *\
        |*            SHORTCUTS            *|
        \* * * * * * * * * * * * * * * * * */

        public User User { get; set; } = null!;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public AuthToken(Guid userId)
        {
            // Inputs
            {
                UserId = userId;
                IssuedAt = DateTimeOffset.UtcNow;
                ExpiresAt = IssuedAt.AddMonths(4);
            }
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*             OVERRIDE            *|
        \* * * * * * * * * * * * * * * * * */

        public override void CopyTo(ref Entity entity)
        {
            if (entity is AuthToken authToken)
            {
                authToken.AccessToken = AccessToken;
                authToken.AccessIV = AccessIV;
                authToken.DiscordToken = DiscordToken;
                authToken.DiscordIV = DiscordIV;
            }
            else
            {
                throw new ArgumentException("Cannot copy to a different type.");
            }
        }

        public override bool Equals(object? obj)
        {
            // Check for null and compare run-time types.
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                AuthToken? authToken = obj as AuthToken;

                return authToken?.UserId.Equals(UserId) ?? false;
            }
        }

        public override int GetHashCode()
        {
            return UserId.GetHashCode();
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                         OPERATORS OVERLOAD                        *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public static bool operator ==(AuthToken? x, AuthToken? y)
        {
            if (x is null && y is null)
            {
                return true;
            }
            else
            {
                return x?.Equals(y) ?? false;
            }
        }

        public static bool operator !=(AuthToken? x, AuthToken? y) => !(x == y);
    }
}
