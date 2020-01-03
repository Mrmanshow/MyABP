using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MyABP.Authorization;
using MyABP.Operation.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyABP.Operation
{
    [AbpAuthorize(PermissionNames.Pages_Operation)]
    public class ArticleAppService: AsyncCrudAppService<Article, ArticleDto, int, PagedArticleResultRequestDto>, IArticleAppService
    {
        private readonly IRepository<Article> _repository;

        public ArticleAppService(IRepository<Article> repository)
            :base(repository)
        {
            this._repository = repository;
        }

        protected override IQueryable<Article> CreateFilteredQuery(PagedArticleResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), (x) => x.Title.Contains(input.Keyword))
                .Select(x => new Article
                {
                    Id = x.Id,
                    Title = x.Title,
                    Img = x.Img,
                    Sort = x.Sort,
                    Type = x.Type,
                    Status = x.Status,
                    CreationTime = x.CreationTime
                });
        }

        /// <summary>
        /// 获取文章类型
        /// </summary>
        /// <returns></returns>
        public IList<EnumEntity> GetArticleType()
        {
            var list = new List<EnumEntity>();

            foreach (var e in Enum.GetValues(typeof(ArticleType)))
            {
                EnumEntity m = new EnumEntity();

                m.Value = Convert.ToInt32(e);
                m.Name = e.ToString();
                list.Add(m);
            }
            return list;
        }
    }
}
