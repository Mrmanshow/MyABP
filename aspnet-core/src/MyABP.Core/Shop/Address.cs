using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyABP.Shop
{
    public class Address : AuditedEntity<int>
    {
        [Required]
        public long UserId { set; get; }

        [Required]
        [StringLength(20)]
        public string Name { set; get; }

        [Required]
        [StringLength(20)]
        public string Phone { set; get; }

        [Required]
        [StringLength(100)]
        public string DetailAddress { set; get; }

        [Required]
        public int Status { set; get; }
    }
}
