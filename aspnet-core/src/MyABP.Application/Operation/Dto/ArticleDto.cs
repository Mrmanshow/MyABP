using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyABP.Operation.Dto
{
    [AutoMapFrom(typeof(Article))]
    public class ArticleDto: EntityDto<int>
    {
        [MaxLength(50)]
        public string Title { set; get; }

        [MaxLength(200)]
        public string Img { set; get; }

        public int Sort { set; get; }

        [Required]
        public int Type { set; get; }

        [Required]
        public int Status { set; get; }

        public DateTime CreationTime { set; get; }
    }
}
