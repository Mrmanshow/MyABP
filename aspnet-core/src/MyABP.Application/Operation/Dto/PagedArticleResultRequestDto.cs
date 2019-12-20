using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyABP.Operation.Dto
{
    public class PagedArticleResultRequestDto: PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
