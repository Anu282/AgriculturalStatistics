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
        public ApplicationDBContext dbContext;
        
        /// <summary>
        ///  Constructor to initialize the connection to the data source
        /// </summary>

        private readonly ILogger<HomeController> _logger;

         public HomeController(ApplicationDBContext context)
         {
             dbContext = context;
         }

        /*public HomeController()
        {
            
        }*/
        public IActionResult Index()
        {
            var groups = (from g in dbContext.Groups
                          join c in dbContext.Commodities on g.GroupID equals c.Group.GroupID
                          select g.GroupName);
            groups = groups.Distinct();

            List<string> Group = new List<string>(groups.Count());
            foreach(var item in groups)
            {
                if (item == "FRUIT & TREE NUTS")
                    Group.Add("FruitsandNuts");
                else if(item=="VEGETABLES" || item=="DAIRY")
                    Group.Add(item);
            }
            ViewData["Groups"] = Group;
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
