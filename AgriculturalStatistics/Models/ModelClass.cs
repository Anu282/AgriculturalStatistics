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
        [Display(Name = "Sector Name")]
        public string SectorName { get; set; }
    }
    public class Group: IEqualityComparer<Group>
    {
        [Key]
        public int GroupID { get; set; }
        [Required]
        [Display(Name = "Group Name")]
        public string GroupName { get; set; }

        public bool Equals(Group x, Group y) { return x.GroupID == y.GroupID && x.GroupID == y.GroupID; }
        public int GetHashCode(Group obj) { return obj.GroupID; }

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
        //[RegularExpression(@"^(\d{4})$", ErrorMessage = "Enter a valid 4 digit Year")]
        [Display(Name = "Year")]
        public string Year { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Geography")]
        public string Geography { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Domain")]
        public string Domain { get; set; }
        [Required]
       
        [Display(Name = "Value")]
        public double Value { get; set; }
        [Required]
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


