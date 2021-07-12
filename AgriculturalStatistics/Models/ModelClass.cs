using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AgriculturalStatistics.Models
{
    public class ModelClass
    {
    }

    public class Sector
    {
        [Key]
        public int SectorID { get; set; }
        [Required]
        public string SectorName { get; set; }
    }
    public class Group
    {
        [Key]
        public int GroupID { get; set; }
        [Required]
        public string GroupName { get; set; }

    }

    public class Commodity
    {
        [Key]
        public int CommodityID { get; set; }
        [Required]
        public string DataItem { get; set; }
        [Required]
        public string CommodityName { get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        public string Geography { get; set; }
        [Required]
        public double Value { get; set; }
        [Required]
        public double CV { get; set; }
        [Required]
        public Group Group { get; set; }
        [Required]
        public Sector Sector { get; set; }
    }

    public class Commodities
    {
        public Commodity[] Commoditylist { get; set; }
    }
}


