using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyABP.Operation.Dto
{
    public class PagedAndSortedBannerResultRequestDto: PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }

        public DateTime? From { set; get; }

        public DateTime? To { set; get; }
    }
}
