using MyABP.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyABP.Shop
{
    public class ProductOrder : AuditedEntity<int>
    {
        [Required]
        [StringLength(20)]
        public string OrderCode { set; get; }

        [Required]
        public long UserId { set; get; }

        [Required]
        public int ProductId { set; get; }

        [StringLength(50)]
        public string ProductName { set; get; }

        [Required]
        public int Price { set; get; }

        [Required]
        public int Count { set; get; }

        public int CutPrice { set; get; }

        [Required]
        public int Amount { set; get; }

        public int? AddressId { set; get; }

        [StringLength(50)]
        public string Coupon { set; get; }

        [StringLength(30)]
        public string ExpressName { set; get; }

        [StringLength(30)]
        public string ExpressCode { set; get; }

        public int Freight { set; get; }

        [Required]
        public int Status { set; get; }

        public long Idx { set; get; }

        [StringLength(100)]
        public string Remarks { set; get; }

        [StringLength(100)]
        public string ContactWay { set; get; }

        public int ProductType { set; get; }

        public decimal PayBalance { set; get; }

        public int PayIntegration { set; get; }

        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { set; get; }

    }
}
