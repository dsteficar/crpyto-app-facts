using Domain.Entity.Users;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using WebAdminUI.States;

namespace WebAdminUI.Components.Pages.Settings
{
    public partial class SettingsPage
    {
        public UserSettings UserSettings { get; set; } = null!;
        private ClaimsPrincipal UserClaims { get; set; } = null!;

        private bool IsDarkTheme = false;
        private bool IsInitialized;

        protected override async Task OnInitializedAsync()
        {
            var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthStateProvider;
            var authState = await customAuthStateProvider.GetAuthenticationStateAsync();
            var userClaims = authState.User;

            var emailClaim = userClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

            if (emailClaim == null) return;

            var getUserSettingsResult = await UserSettingsService.GetByUserEmail(emailClaim.Value);

            if (!getUserSettingsResult.IsSuccess) return;

            UserSettings = getUserSettingsResult.Value;
            if (IsInitialized) return;
            IsDarkTheme = UserSettings.Theme == "dark" ? true : false;
         }


        private async Task ToggleThemeAsync()
        {
            IsDarkTheme = !IsDarkTheme;
            UserSettings.Theme = IsDarkTheme == true ? "dark" : "light";

            var updateUserSettingsResult = await UserSettingsService.UpdateAsync(UserSettings);

            NavManager.NavigateTo("/settings", forceLoad: true);
        }
    }
}
