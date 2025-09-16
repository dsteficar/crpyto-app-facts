using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace WebApplicationUI.Layout
{
    public partial class NavMenu
    {
        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        private bool collapseNavMenu = true;
        private bool IsAnonymousUser = true;
        private string? NavMenuCssClass => collapseNavMenu ? null : "expanded"; // Update the CSS class dynamically based on the collapsed state

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateTask;
            var user = authState.User;

            var IsAuthenticated = user.Identity?.IsAuthenticated ?? false;

            if (IsAuthenticated && !user.IsInRole("Anonymous"))
            {
                IsAnonymousUser = false;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender) // Toggle method to collapse or expand the menu
        {
            if (!firstRender && !collapseNavMenu)
            {
                await jsRuntime.InvokeVoidAsync("applySlideInAnimation");
            }
        }
        
        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
            StateHasChanged();  // Force re-render to apply changes
        }

        private async Task HandleLogout()
        {
            await AccountClientService.LogoutAsync();
            NavManager.NavigateTo("/login", forceLoad: true);
        }
    }
}
