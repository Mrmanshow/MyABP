using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyABP.Shop.Dto
{
    public class PagedProductOrderResultRequestDto : PagedAndSortedResultRequestDto, IPagedAndSortedResultRequest
    {
        public string Keyword { get; set; }
        public int Status { get; set; }
    }
}
