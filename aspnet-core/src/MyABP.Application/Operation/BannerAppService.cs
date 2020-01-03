using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using MyABP.Authorization;
using MyABP.Operation.Dto;
using MyABP.Users.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MyABP.Configuration;
using Abp.Localization;

namespace MyABP.Operation
{
    [AbpAuthorize(PermissionNames.Pages_Operation)]
    public class BannerAppService: AsyncCrudAppService<Banner, BannerDto, int, PagedAndSortedBannerResultRequestDto, BannerInput>, IBannerAppService
    {
        private readonly IRepository<Banner> _bannerRepository;

        public BannerAppService(IRepository<Banner> bannerRepository)
            :base(bannerRepository)
        {
            this._bannerRepository = bannerRepository;

            CreatePermissionName = "Pages.Operation";
        }

        public override async Task<BannerDto> Create(BannerInput input)
        {
            var banner = ObjectMapper.Map<Banner>(input);
            //banner.BannerImg = "";
            banner = await this._bannerRepository.InsertAsync(banner);

            return MapToEntityDto(banner);
        }

        public override async Task<BannerDto> Update(BannerInput input)
        {
            CheckUpdatePermission();

            var banner = await _bannerRepository.GetAsync(input.Id);
            MapToEntity(input, banner);

            await this._bannerRepository.UpdateAsync(banner);

            return await Get(input);
        }

        protected override IQueryable<Banner> ApplySorting(IQueryable<Banner> query, PagedAndSortedBannerResultRequestDto input)
        {
            return query.OrderByDescending(r => r.BannerOrder).OrderByDescending(r => r.CreationTime);
        }

        protected override IQueryable<Banner> CreateFilteredQuery(PagedAndSortedBannerResultRequestDto input)
        {
            return Repository.GetAll().WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Theme.Contains(input.Keyword))
                .WhereIf(input.From.HasValue, x => x.CreationTime >= input.From.Value)
                .WhereIf(input.To.HasValue, x => x.CreationTime < input.To.Value.AddDays(1));
        }

        /// <summary>
        /// 获取轮播图类型列表
        /// </summary>
        /// <returns></returns>
        public IList<EnumEntity> GetBannerType()
        {
            var list = new List<EnumEntity>();

            foreach (var e in Enum.GetValues(typeof(BannerType)))
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
