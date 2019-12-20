using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using MyABP.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyABP.Game.Laba.Dto
{
    [AutoMapFrom(typeof(LabaOrder))]
    public class LabaOrderDto: EntityDto<int>
    {
        public string UserName { set; get; }

        public int Amount { set; get; }

        public int WinAmount { set; get; }

        public string Position { set; get; }

        public DateTime CreationTime { get; set; }
    }
}
