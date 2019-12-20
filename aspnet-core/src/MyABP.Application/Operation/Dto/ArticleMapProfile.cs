using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyABP.Operation.Dto
{
    public class ArticleMapProfile : Profile
    {
        public ArticleMapProfile()
        {
            CreateMap<ArticleDto, Article>();
        }
    }
}
