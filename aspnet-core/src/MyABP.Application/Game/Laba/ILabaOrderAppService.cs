using Abp.Application.Services;
using MyABP.Game.Laba.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyABP.Game.Laba
{
    public interface ILabaOrderAppService : IAsyncCrudAppService<LabaOrderDto, int, PagedLabaOrderResultRequestDto>
    {
    }
}
