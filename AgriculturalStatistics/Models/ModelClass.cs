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
        [Display(Name ="DataItem")]
        public string DataItem { get; set; }
        [Required]
        [Display(Name = "Commodity")]
        public string CommodityName { get; set; }
        
        [Display(Name = "Year")]
        public string Year { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Geography")]
        public string Geography { get; set; }
        [Required]
        [RegularExpression(@"^\d+\.\d{0}$")]
        [Range(0, 9999999999999999)]
        [Display(Name = "Value")]
        public double Value { get; set; }
        [Required]
        [RegularExpression(@"^\d+\.\d{0}$")]
        [Range(0, 9999999999999999)]
        [Display(Name = "CV")]
        public double CV { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Group")]
        public Group Group { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Sector")]
        public Sector Sector { get; set; }
    }

    public class Commodities
    {
        public Commodity[] Commoditylist { get; set; }
    }
}


