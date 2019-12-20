using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyABP.Operation.Dto
{
    [AutoMapTo(typeof(Banner))]
    public class BannerInput: EntityDto<int>
    {
        public string BannerImg { set; get; }

        public string BannerLink { set; get; }

        public int BannerOrder { set; get; }

        [Required]
        public int Status { set; get; }

        [Required]
        public int Type { set; get; }

        public int LinkType { set; get; }

        public string Theme { set; get; }

        [Required]
        public DateTime ShowBeginDate { set; get; }

        [Required]
        public DateTime ShowEndDate { set; get; }
    }
}
