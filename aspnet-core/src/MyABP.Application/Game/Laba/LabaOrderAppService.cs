using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.BackgroundJobs;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.ObjectMapping;
using Abp.Quartz;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyABP.Authorization;
using MyABP.Authorization.Users;
using MyABP.Game.Laba.Dto;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyABP.Game.Laba
{
    [AbpAuthorize(PermissionNames.Pages_Games)]
    public class LabaOrderAppService: AsyncCrudAppService<LabaOrder, LabaOrderDto, int, PagedLabaOrderResultRequestDto>, ILabaOrderAppService
    {
        private readonly IRepository<LabaOrder, int> _repository;
        private readonly ILabaOrderManger _labaOrderManger;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IQuartzScheduleJobManager _jobManager;

        public LabaOrderAppService(IRepository<LabaOrder, int> repository, 
            UserManager userManager, 
            IObjectMapper objectMapper,
            ILabaOrderManger labaOrderManger,
            IBackgroundJobManager backgroundJobManager,
            IQuartzScheduleJobManager jobManager)
            :base(repository)
        {
            this._repository = repository;
            this._labaOrderManger = labaOrderManger;
            this._backgroundJobManager = backgroundJobManager;
            this._jobManager = jobManager;
        }

        protected override IQueryable<LabaOrder> CreateFilteredQuery(PagedLabaOrderResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Users)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Users.UserName.Contains(input.Keyword))
                .WhereIf(input.From.HasValue, x => x.CreationTime > input.From.Value)
                .WhereIf(input.To.HasValue, x => x.CreationTime < input.To.Value.AddDays(1));
        }

        public async Task<CreateLabaOrderResultOutput> CreateLabaOrder(CreateLabaOrderInput input)
        {
            int amount = 0;
            string[] listRoute = input.Routes.Split('|');
            int index = 0;
            string ids = "";
            string strId = "|";
            int price = 0;
 
            foreach (string item in listRoute)
            {
                index = item.IndexOf('-');
                strId = item.Substring(0, index);
                // 线路id不能重复
                if (!ids.Contains("|" + strId + "|"))
                {
                    ids += item.Substring(0, index) + "|";
                }
                else
                {
                    return new CreateLabaOrderResultOutput { Status = 3, Tip = "參數錯誤" };
                }
                price = int.Parse(item.Substring(index + 1, item.Length - index - 1));
                if (price <= 0)
                {
                    return new CreateLabaOrderResultOutput { Status = 3, Tip = "參數錯誤" };
                }
                amount += price;
            }

            var order = new LabaOrder
            {
                Amount = amount,
                CreatorUserId = AbpSession.UserId
            };

            int id = await _labaOrderManger.CreateLabaOrder(order, input.Routes);

            if (id > 0)
            {
                var result = await _labaOrderManger.LabaOrderCal(order.Id);
                //await _backgroundJobManager.EnqueueAsync<LabaCalResultJob, LabaOrderCalArgs>(
                //new LabaOrderCalArgs
                //{
                //    UserId = AbpSession.UserId.Value,
                //    Position = result,
                //    OrderId = order.Id
                //});

                IDictionary<string, object> data = new Dictionary<string, object>();
                data.Add("UserId", AbpSession.UserId.Value);
                data.Add("Position", result);
                data.Add("OrderId", order.Id);

                await _jobManager.ScheduleAsync<AbpQuartzLabaCalResultJob>(
                  job =>
                  {
                      
                      JobDataMap map = new JobDataMap(data);
                      job.UsingJobData(map);
                            
                  },
                  trigger =>
                  {
                      trigger.StartNow();
                  });

                return new CreateLabaOrderResultOutput
                {
                    Status = 0,
                    WinIndex = result,
                    Tip = "下单成功"
                };
            }
            else
            {
                return new CreateLabaOrderResultOutput
                {
                    Status = id * -1,
                    Tip = "下单失败"
                };
            }


        }

    }
}
