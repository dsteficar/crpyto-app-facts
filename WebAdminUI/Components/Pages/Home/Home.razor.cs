using Application.Services;
using Domain.Entity.Authentication;
using Radzen;
using System.ComponentModel.Design;
using System.Globalization;
using System.Security.Claims;
using WebAdminUI.States;

namespace WebAdminUI.Components.Pages.Home
{
    public partial class Home
    {
        private bool isInitialized;
        private bool isAuthenticated;
        private string userRole;

        protected override async Task OnInitializedAsync()
        {
            var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthStateProvider;
            var authState = await customAuthStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;


        }

        //private async Task ToggleThemeAsync()
        //{

        //    var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthStateProvider;
        //    var authState = await customAuthStateProvider.GetAuthenticationStateAsync();
        //    var user = authState.User;

        //    var emailClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

        //    if (emailClaim == null) return;

        //    var getUserSettingsResult = await UserSettingsService.GetByUserEmail(emailClaim.Value);

        //    if (!getUserSettingsResult.IsSuccess) return;

        //    var userSettings = getUserSettingsResult.Value;

        //    userSettings.Theme = userSettings.Theme == "dark" ? "light" : "dark";

        //    var updateUserSettingsResult = await UserSettingsService.UpdateAsync(userSettings);

        //    NavManager.NavigateTo("/home");
        //}

        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if (firstRender)
        //    {
        //        await ThemeService.InitializeThemeAsync();
        //        await ThemeService.ListenForThemeChangesAsync();
        //        ThemeService.OnThemeChanged += StateHasChanged;
        //    }

        //}

        //private void ToggleTheme()
        //{
        //    ThemeService.IsDarkMode = !ThemeService.IsDarkMode;
        //}

        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if (firstRender)
        //    {

        //        await AppTheme.ListenForThemeChanges(JSRuntime);
        //        AppTheme.OnChange += StateHasChanged;

        //    }
        //    //if (firstRender)
        //    //{
        //    //    //await homeClientService.GetIndexPage();
        //    //    isInitialized = true;
        //    //    StateHasChanged();
        //    //}
        //}

        //void ToggleTheme()
        //{
        //    AppTheme.IsDarkMode = !AppTheme.IsDarkMode;
        //}
    }
}
