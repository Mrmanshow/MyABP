using Abp.Application.Services;
using Microsoft.AspNetCore.Http;
using MyABP.Operation.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyABP.Operation
{
   
    public interface IBannerAppService : IAsyncCrudAppService<BannerDto, int, PagedAndSortedBannerResultRequestDto, BannerInput>
    {
        IList<EnumEntity> GetBannerType();
    }
}
