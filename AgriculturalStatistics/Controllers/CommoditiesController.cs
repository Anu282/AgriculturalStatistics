using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgriculturalStatistics.DataAccess;
using Microsoft.EntityFrameworkCore;
using AgriculturalStatistics.ViewModel;
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
            var vegetables = _context.Commodities.Include(p => p.Group).Include(p => p.Sector).Where(c => c.Group.GroupID == 20).ToList();
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

            var sector = _context.Sectors.ToList();
            var group = _context.Groups.ToList();

            var viewmodel = new CommodityViewModel()
            {   Commodity = new Commodity(),
                GroupList = group,
                SectorList = sector
            };
            return View(viewmodel);

        }
       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CommodityViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Commodity.Group = _context.Groups.Where(p => p.GroupName == model.Commodity.Group.GroupName).FirstOrDefault();
                    model.Commodity.Sector = _context.Sectors.Where(p => p.SectorName == model.Commodity.Sector.SectorName).FirstOrDefault();
                    var commodity = model.Commodity.Group.GroupName;

                    _context.Commodities.Add(model.Commodity);
                    _context.SaveChanges();

                    if (commodity == "VEGETABLES")

                        return RedirectToAction("Vegetables", "Commodities");

                    else if (commodity == "FRUIT & TREE NUTS")

                        return RedirectToAction("FruitsandNuts", "Commodities");

                    else if (commodity == "DAIRY")

                        return RedirectToAction("Dairy", "Commodities");

                    else
                        return RedirectToAction("Vegetables", "Commodities");
                }
                else
                {
                    
                        var viewModel = new CommodityViewModel
                        {
                            Commodity = model.Commodity,
                            GroupList = _context.Groups.ToList(),
                            SectorList=_context.Sectors.ToList()
                        };

                        return View("Create", viewModel);
                    
                }
                
            }
            catch(DbUpdateException)
            {
                ModelState.AddModelError("", "Error Occured");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var Commodity = _context.Commodities.SingleOrDefault(c => c.CommodityID == id);
            var sector = _context.Sectors.ToList();
            var group = _context.Groups.ToList();
            if (Commodity == null)
                return NotFound();
            else
            {
               
                var viewmodel = new CommodityViewModel()
                {
                    Commodity = Commodity,
                    GroupList = group,
                    SectorList = sector


                };
                return View(viewmodel);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ViewModel.CommodityViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Commodity.Group = _context.Groups.Where(p => p.GroupName == model.Commodity.Group.GroupName).FirstOrDefault();
                    model.Commodity.Sector = _context.Sectors.Where(p => p.SectorName == model.Commodity.Sector.SectorName).FirstOrDefault();
                    var commodityindb = _context.Commodities.Single(c => c.CommodityID == model.Commodity.CommodityID);
                    commodityindb.CommodityName = model.Commodity.CommodityName;
                    commodityindb.DataItem = model.Commodity.DataItem;
                    commodityindb.Geography = model.Commodity.Geography;
                    commodityindb.CV = model.Commodity.CV;
                    commodityindb.Value = model.Commodity.Value;
                    commodityindb.Group = model.Commodity.Group;
                    commodityindb.Sector = model.Commodity.Sector;
                    _context.Commodities.Update(commodityindb);
                    _context.SaveChanges();
                    if (commodityindb.Group.GroupName == "VEGETABLES")

                        return RedirectToAction("Vegetables", "Commodities");

                    else if (commodityindb.Group.GroupName == "FRUIT & TREE NUTS")

                        return RedirectToAction("FruitsandNuts", "Commodities");

                    else if (commodityindb.Group.GroupName == "DAIRY")

                        return RedirectToAction("Dairy", "Commodities");

                    else
                        return RedirectToAction("Vegetables", "Commodities");
                }
                else
                {
                    var viewModel = new CommodityViewModel
                    {
                        Commodity = model.Commodity,
                        GroupList = _context.Groups.ToList(),
                        SectorList = _context.Sectors.ToList()
                    };

                    return View("Edit", viewModel);
                }
            }
            catch(DbUpdateException)
            {
                ModelState.AddModelError("", "Error Occured");
            }
            return View(model);

        }

        public IActionResult Delete(int id)
        {
            var Commodity = _context.Commodities.Include(c=>c.Group).SingleOrDefault(c => c.CommodityID == id);
            _context.Commodities.Remove(Commodity);
            _context.SaveChanges();
            if (Commodity.Group.GroupName == "VEGETABLES")

                return RedirectToAction("Vegetables", "Commodities");

            else if (Commodity.Group.GroupName == "FRUIT & TREE NUTS")

                return RedirectToAction("FruitsandNuts", "Commodities");

            else if (Commodity.Group.GroupName == "DAIRY")

                return RedirectToAction("Dairy", "Commodities");

            else
                return RedirectToAction("Vegetables", "Commodities");
        }

        public IActionResult Aboutus()
        {
            
            return View();
        }
        public IActionResult statistics()
        {

            return View();
        }
    }
}
