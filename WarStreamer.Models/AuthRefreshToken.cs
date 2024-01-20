using System.ComponentModel.DataAnnotations;
using WarStreamer.Models.EntityBase;

namespace WarStreamer.Models
{
    public class AuthRefreshToken : Entity
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [Key]
        public Guid UserId { get; private set; }

        public string TokenValue { get; set; } = null!;

        public string AesInitializationVector { get; set; } = null!;

        public DateTimeOffset IssuedAt { get; private set; }

        public DateTimeOffset ExpiresAt { get; private set; }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public AuthRefreshToken(Guid userId)
        {
            // Inputs
            {
                UserId = userId;
                IssuedAt = DateTimeOffset.UtcNow;
                ExpiresAt = IssuedAt.AddMonths(1);
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
            if (entity is AuthRefreshToken authToken)
            {
                authToken.TokenValue = TokenValue;
                authToken.AesInitializationVector = AesInitializationVector;
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
                AuthRefreshToken? authToken = obj as AuthRefreshToken;

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

        public static bool operator ==(AuthRefreshToken? x, AuthRefreshToken? y)
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

        public static bool operator !=(AuthRefreshToken? x, AuthRefreshToken? y) => !(x == y);
    }
}
