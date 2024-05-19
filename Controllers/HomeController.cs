using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportClubs1.Data;
using SportClubs1.Models;
using System.Diagnostics;

namespace SportClubs1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public readonly SportClubsContext _context;
        public HomeController(SportClubsContext context)
        {
            _context = context;
        }
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                Gyms = _context.Gyms.ToList(),
                Staffs = _context.Staff.ToList(),
                TrainingMachines = _context.TrainingMachines.ToList(),
                Clients = _context.Clients.ToList()
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
