using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyABP.Shop.Dto
{
    [AutoMapTo(typeof(ProductOrder))]
    public class ShopOrderCreateInput: EntityDto<int>
    {
        [Required]
        public int ProductId { set; get; }

        [Required]
        public int Count { set; get; }

        [StringLength(100)]
        public string Remarks { set; get; }

        [Required]
        public int PayIntegration { set; get; }

        [Required]
        [StringLength(30)]
        public string PayPassword { set; get; }

        public int Idx { set; get; }

        public int? AddressId { set; get; }

        [StringLength(100)]
        public string ContactWay { set; get; }
    }
}
