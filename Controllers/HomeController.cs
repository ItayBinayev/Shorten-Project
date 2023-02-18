using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShortenProject.Database;
using ShortenProject.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace ShortenProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UrlDbcontext _dbcontext;

        public HomeController(UrlDbcontext dbcontext, ILogger<HomeController> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

		[Authorize]
		public IActionResult Links()
		{
            var currentUser = HttpContext.User;
            var userId = currentUser.FindFirstValue(ClaimTypes.NameIdentifier);
            var tempUser = _dbcontext.Users.FirstOrDefault(u => u.Id == userId);
            if (tempUser != null)
            {
                var data = _dbcontext.URLs.Where(u => u.UrlUser.Equals(tempUser)).ToList();
                return View(data);
            }
            else {
            var data2 = _dbcontext.URLs.Where(u => u.UrlUser == null).ToList();
            return View(data2);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}