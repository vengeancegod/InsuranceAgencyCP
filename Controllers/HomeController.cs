using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsuranceAgency__.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using DocumentFormat.OpenXml.Spreadsheet;

namespace InsuranceAgency__.Controllers
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
            try
            {
                _logger.LogInformation("Домашний контроллер успешно запущен!");
            }
            catch (Exception)
            {
                _logger.LogError("Домашний контроллер не был запущен. Вызвано исключение!");
            }
            return View();
        }
        public IActionResult Privacy()
        {
            try
            {
                _logger.LogInformation("Контроллер справочных услуг успешно запущен!");
            }
            catch (Exception)
            {
                _logger.LogError("Контроллер справочных услуг не был запущен. Вызвано исключение!");
            }
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
