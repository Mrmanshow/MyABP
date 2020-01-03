using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyABP.Shop.Dto
{
    [AutoMapFrom(typeof(ProductOrder))]
    public class ProductOrderDto: EntityDto<int>
    {
        [Required]
        [StringLength(20)]
        public string OrderCode { set; get; }

        [StringLength(50)]
        public string ProductName { set; get; }

        public string Name { set; get; }

        [Required]
        public int Count { set; get; }

        [Required]
        [StringLength(20)]
        public string Phone { set; get; }

        [StringLength(100)]
        public string DetailAddress { set; get; }

        [Required]
        public int Amount { set; get; }

        public DateTime CreationTime { set; get; }

        [StringLength(50)]
        public string Coupon { set; get; }

        [StringLength(30)]
        public string ExpressCode { set; get; }

        [Required]
        public int Status { set; get; }

        public decimal PayBalance;

        public int PayIntegration;
    }
}
