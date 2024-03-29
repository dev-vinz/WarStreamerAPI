﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WarStreamer.Models.EntityBase;

namespace WarStreamer.Models
{
    public class User : Entity
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; private set; }

        public Guid LanguageId { get; set; }

        public uint TierLevel { get; set; }

        public bool NewsLetter { get; set; }

        /* * * * * * * * * * * * * * * * * *\
        |*            SHORTCUTS            *|
        \* * * * * * * * * * * * * * * * * */

        public AuthToken AuthToken { get; set; } = null!;

        public ICollection<Image> Images { get; set; } = new List<Image>();

        public Language Language { get; set; } = null!;

        public OverlaySetting? OverlaySetting { get; set; }

        public ICollection<Account> Accounts { get; set; } = new List<Account>();

        public ICollection<TeamLogo> TeamLogos { get; set; } = new List<TeamLogo>();

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public User(Guid id)
        {
            // Inputs
            {
                Id = id;
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
            if (entity is User user)
            {
                user.LanguageId = LanguageId;
                user.TierLevel = TierLevel;
                user.NewsLetter = NewsLetter;
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
                User? user = obj as User;

                return user?.Id.Equals(Id) ?? false;
            }
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                         OPERATORS OVERLOAD                        *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public static bool operator ==(User? x, User? y)
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

        public static bool operator !=(User? x, User? y) => !(x == y);
    }
}
