using Abp.Application.Services;
using MyABP.Shop.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyABP.Shop
{
    public interface IShopOrderAppService: IAsyncCrudAppService<ProductOrderDto, int, PagedProductOrderResultRequestDto, ShopOrderCreateInput>
    {
    }
}
