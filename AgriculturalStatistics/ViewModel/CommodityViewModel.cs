using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgriculturalStatistics.Models;

namespace AgriculturalStatistics.ViewModel
{
    public class CommodityViewModel
    {
        public Commodity Commodity { get; set; }
        public IEnumerable<Group> GroupList { get; set; }
        public IEnumerable<Sector> SectorList { get; set; }
    }
}
