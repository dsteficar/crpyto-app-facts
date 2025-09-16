using WebAdminUI.Services.Tokens;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using WebAdminUI.States;
using Microsoft.AspNetCore.Components.Authorization;

namespace WebAdminUI.Services.Home
{
    public class HomeClientService : BaseClientServiceOLD, IHomeClientService
    {
        private const string BaseUrl = "api/home";

        public HomeClientService(
            HttpClient httpClient,
            ITokenClientService tokenClientService,
            NavigationManager navigationManager
            ) : base(httpClient, tokenClientService, navigationManager)
        {
        }

        public async Task<ActionResult> GetIndexPage()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/index");
                await SendRequestAsync(request);
                return new OkResult();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request failed: {ex.Message}");
                return new StatusCodeResult(500);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new StatusCodeResult(500);
            }
        }
    }
}
