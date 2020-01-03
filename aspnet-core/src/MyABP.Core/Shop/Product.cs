using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyABP.Shop
{
    public class Product : AuditedEntity<int>
    {
        [Required]
        [StringLength(50)]
        public string Name { set; get; }

        [StringLength(100)]
        public string Brief { set; get; }

        [Required]
        public int Category { set; get; }

        [Required]
        public int Amount { set; get; }

        [Required]
        public int SoldCount { set; get; }

        [Required]
        public int Stock { set; get; }

        [Required]
        public int Status { set; get; }

        public int Freight { set; get; }

        public int MarketPrice { set; get; }

        [Required]
        public int Type { set; get; }
    }
}
