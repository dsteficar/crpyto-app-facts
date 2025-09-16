using WebApplicationUI.States;

namespace WebApplicationUI.Pages.Account
{
    public partial class MyAccountPage
    {
        private string NameField { get; set; } = string.Empty;
        private string EmailField { get; set; } = string.Empty;
        private string BinanceAPIKeyField { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthStateProvider;
            var test = await customAuthStateProvider.GetAuthenticationStateAsync();

            // var test = AuthStateProvider.GetAuthenticationStateAsync();
            // NameField = authStateProvider.GetUserClaimPrincipalName();
            // EmailField = authStateProvider.GetUserClaimPrincipalName();
        }

        private void OnSubmit()
        {
        }

        private void NavigateToChangePassword()
        {
            navManager.NavigateTo("/change-password");
        }
    }
}
