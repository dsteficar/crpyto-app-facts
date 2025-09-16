using Microsoft.AspNetCore.Mvc;

namespace WebAdminUI.Services.Home
{
    public interface IHomeClientService
    {
        Task<ActionResult> GetIndexPage();
    }
}
