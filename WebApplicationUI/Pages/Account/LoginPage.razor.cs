using Application.DTOs.Account.Requests;
using Application.DTOs.Account.Responses;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebApplicationUI.Pages.Account
{
    public partial class LoginPage
    {
        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        private LoginRequestDTO loginModel { get; set; } = new LoginRequestDTO();

        private string emailRegex = "[^@ \\t\\r\\n]+@[^@ \\t\\r\\n]+\\.[^@ \\t\\r\\n)]+";
        private string passwordRegex = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,15}$";

        private bool hidePassword = true;
        private bool showAlertMessage = false;

        private string passwordRgxValidationMessage = "Password must be between 8 and 15 characters and contain at least one lowercase letter, one uppercase letter, one numeric digit, and one special character";
        private string emailRgxValidationMessage = "Invalid Email Address format.";
        private string passwordReqValidationMessage = "Password is required!";
        private string emailReqValidationMessage = "Email address is required!";
        private string errorMessage = string.Empty;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                // // isInitialized = true;
                // // var response = await accountClientService.LoginWithAccessTokenAsync(); //Maybe update access token?

                // // if (!string.IsNullOrEmpty(response))
                // // {
                // //     var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthStateProvider;
                // //     var currentAccessToken = await tokenClientService.GetAccessTokenAsync();
                // //     customAuthStateProvider.UpdateAuthenticationState(currentAccessToken!);

                // //     NavManager.NavigateTo("/home", forceLoad: true);
                // // }

                //StateHasChanged();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateTask;
            var user = authState.User;

            var IsAuthenticated = user.Identity?.IsAuthenticated ?? false;

            if (IsAuthenticated && !user.IsInRole("Anonymous"))
            {
                NavManager.NavigateTo("/home", forceLoad: true);
            }
        }

        private async Task HandleLogin()
        {
            try
            {
                LoginResponseDTO response = await accountClientService.LoginAsync(loginModel);

                if (string.IsNullOrEmpty(response.AccessToken) || string.IsNullOrEmpty(response.RefreshToken))
                {
                    await ShowErrorMessage("Invalid username or password.");
                    return;
                }

                NavManager.NavigateTo("/home", forceLoad: true);

            }
            catch (Exception ex)
            {
                await ShowErrorMessage(ex.Message);
            }
        }

        private void TogglePassword()
        {
            hidePassword = !hidePassword;
        }

        private async Task ShowErrorMessage(string message)
        {
            await jsRuntime.InvokeVoidAsync("alert", message);
        }
    }
}
