using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyABP.Operation.Dto
{
    [AutoMapFrom(typeof(Banner))]
    public class BannerDto : EntityDto<int>
    {
        public string BannerImg { set; get; }

        public string BannerLink { set; get; }

        public int BannerOrder { set; get; }

        [Required]
        public int Status { set; get; }

        [Required]
        public int Type { set; get; }

        public string Theme { set; get; }

        public DateTime ShowBeginDate { set; get; }

        public DateTime ShowEndDate { set; get; }

        public DateTime CreationTime { set; get; }
    }
}
