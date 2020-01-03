using Abp.Application.Services;
using Microsoft.AspNetCore.Http;
using MyABP.Operation.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyABP.Operation
{
    public interface IArticleAppService: IAsyncCrudAppService<ArticleDto, int, PagedArticleResultRequestDto>
    {
        IList<EnumEntity> GetArticleType();
    }
}
