using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskManager.Models;
using System.Collections.Generic;
using System.Linq;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //using (TasksDbContext db = new TasksDbContext())
            //{
            //    var query = (from t in db.Tasks
            //                select t).ToList();

            //}
            return View();
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