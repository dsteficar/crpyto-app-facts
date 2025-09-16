namespace WebAdminUI.Services.Users
{
    public interface IUserPreferencesService
    {
        string GetPreference(string key);
        void SetPreference(string key, string value);
    }
}
