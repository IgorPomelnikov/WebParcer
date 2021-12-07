using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebParcer.Data.DataProviders;
using WebParcer.Interfaces;
using WebParcer.Models;

namespace WebParcer.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        IAuthable auth;

        public HomeController(ILogger<HomeController> logger, IAuthable auth)
        {
            _logger = logger;
            this.auth = auth;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GroupList(string query, int countOfGroups)
        {
            GroupProvider provider = new(auth.Vk);
            return View(provider.GetFullGroups(provider.GetEmptyGroups(query, countOfGroups)));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
