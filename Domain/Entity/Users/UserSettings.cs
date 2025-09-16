using Domain.Entity.Authentication;

namespace Domain.Entity.Users
{
    public class UserSettings
    {
        public int Id { get; set; } // Primary Key
        public int UserId { get; set; } // FK to your ApplicationUser or IdentityUser
        public string Theme { get; set; } = "light"; // "light" or "dark"
        public string Language { get; set; } = "en"; // Language preference
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public virtual ApplicationUser User { get; set; }
    }
}
