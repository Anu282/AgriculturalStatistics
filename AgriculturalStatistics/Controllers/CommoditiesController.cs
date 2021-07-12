using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgriculturalStatistics.DataAccess;
using Microsoft.EntityFrameworkCore;
using AgriculturalStatistics.Models;

namespace AgriculturalStatistics.Controllers
{
    
   
    public class CommoditiesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public CommoditiesController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Vegetables()
        {
           var vegetables= _context.Commodities.Include(p=>p.Group).Include(p=>p.Sector).Where(c => c.Group.GroupID == 20).ToList();
           ViewData["Vegetables"] = vegetables;
           return View();
        }
        public IActionResult FruitsandNuts()
        {
            var fruitsnuts = _context.Commodities.Include(p => p.Group).Include(p => p.Sector).Where(c => c.Group.GroupID == 10).ToList();
            ViewData["FruitsNuts"] = fruitsnuts;
            return View();
        }
        public IActionResult Dairy()
        {
            var dairy = _context.Commodities.Include(p => p.Group).Include(p => p.Sector).Where(c => c.Group.GroupID == 5).ToList();
            ViewData["Dairy"] = dairy;
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
           /* Dictionary<int, string> group = new Dictionary<int, string>();
            foreach (Group i in _context.Groups)
            {
                group.Add(i.GroupID, i.GroupName);
            }*/
            List<Sector> sector =  _context.Sectors.ToList();
            ViewBag.GroupName = _context.Groups.ToList();
            ViewBag.SectorName = sector;
            return View();
           
        }

        [HttpPost]
        public IActionResult Create(Commodity model)
        {
            _context.Commodities.Add(model);
            _context.SaveChanges();
            ViewBag.Message = "Data Insert Successfully";
            return View();
        }
    }
}
