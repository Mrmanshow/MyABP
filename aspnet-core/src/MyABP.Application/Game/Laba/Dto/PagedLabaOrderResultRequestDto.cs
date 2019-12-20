using Abp.Application.Services.Dto;
using Abp.Timing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyABP.Game.Laba.Dto
{
    [Serializable]
    public class PagedLabaOrderResultRequestDto: PagedResultRequestDto
    {
        public string Keyword { get; set; }

        public DateTime? From { set; get; }

        public DateTime? To { set; get; }

    }
}
