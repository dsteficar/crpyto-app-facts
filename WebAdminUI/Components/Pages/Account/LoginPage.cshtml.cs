using Application.DTOs.Admin.Account.Requests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WebAdminUI.Services.Accounts;

namespace WebAdminUI.Components.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAccountClientService _accountClientService;

        public LoginModel(IAccountClientService accountClientService)
        {
            _accountClientService = accountClientService;
        }

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password)) return Page();

            var loginRequest = new LoginAdminRequestDTO
            {
                EmailAddress = Email,
                Password = Password
            };

            try
            {
                var loginResponse = await _accountClientService.LoginAsync(loginRequest);

                if (loginResponse != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, loginResponse.Email),
                    new Claim(ClaimTypes.Name, loginResponse.Name),
                    new Claim(ClaimTypes.Role, loginResponse.Role)
                };

                    var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync("Cookies",
                        claimsPrincipal,
                        new AuthenticationProperties
                        {
                            IsPersistent = true // Required for setting expiration in cookies
                        }
                    );
                    return Redirect("/home");
                }

                ErrorMessage = "Invalid username or password.";
                return Page();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred: {ex.Message}";
                return Page();
            }
        }
    }
}