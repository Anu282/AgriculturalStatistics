using AgriculturalStatistics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Reflection;
using AgriculturalStatistics.DataAccess;
using AgriculturalStatistics.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace AgriculturalStatistics.Controllers
{
    public class HomeController : Controller
    {
        HttpClient httpClient;
       // public ApplicationDBContext dbContext;
        static string BASE_URL = "http://quickstats.nass.usda.gov/api/api_GET/";
        static string API_KEY = "7887F66A-0938-3A5B-9B7D-8F4524BE5665";
        /// <summary>
        ///  Constructor to initialize the connection to the data source
        /// </summary>

        private readonly ILogger<HomeController> _logger;

        /* public HomeController(ApplicationDBContext context)
         {
             dbContext = context;
         }*/

        public HomeController()
        {
            
        }
        public IActionResult Index()
        {
            
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
