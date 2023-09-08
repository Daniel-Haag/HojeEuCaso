using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HojeEuCaso.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HojeEuCaso.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? month, int? year)
        {
            DateTime currentDate = DateTime.Now;

            if (month.HasValue && year.HasValue)
            {
                currentDate = new DateTime(year.Value, month.Value, 1);

                // Tratar casos onde o mês ou ano excedem os limites
                if (currentDate.Month == 0)
                {
                    currentDate = currentDate.AddYears(-1).AddMonths(12);
                }
                else if (currentDate.Month == 13)
                {
                    currentDate = currentDate.AddYears(1).AddMonths(-12);
                }
            }

            ViewBag.CurrentDate = currentDate;

            return View();
        }



        public IActionResult Contato()
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
