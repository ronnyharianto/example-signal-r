using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR.Hubs;
using SignalR.Models;
using System.Diagnostics;

namespace SignalR.Controllers
{
    public class HomeController(ILogger<HomeController> logger, IHubContext<ChatHub> chatHubContext) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly IHubContext<ChatHub> _chatHubContext = chatHubContext;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            _chatHubContext.Clients.All.SendAsync("ReceiveMessage", "Privacy is opened");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
