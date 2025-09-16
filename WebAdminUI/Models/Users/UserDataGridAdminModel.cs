namespace WebAdminUI.Models.Users
{
    public class UserDataGridAdminModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool EmailConfirmed { get; set; } = false;
        public string PasswordHash { get; set; } = string.Empty;
        public string SecurityStamp { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string PhoneNumberConfirmed { get; set; } = string.Empty;
        public bool TwoFactorEnabled { get; set; } = false;
        public DateTime? LockoutEnd { get; set; } = null;
        public bool LockoutEnabled { get; set; } = true;
        public int AccessFailedCount { get; set; } = 0;
    }
}
