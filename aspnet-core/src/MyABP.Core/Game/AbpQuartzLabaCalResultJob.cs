using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.Events.Bus.Exceptions;
using Abp.Events.Bus.Handlers;
using Abp.Quartz;
using Abp.Runtime.Caching;
using Microsoft.EntityFrameworkCore;
using MyABP.Authorization.Users;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyABP.Game
{
    public class AbpQuartzLabaCalResultJob : JobBase, ITransientDependency
    {
        private readonly IRepository<LabaList> _labaListRepository;
        private readonly IRepository<LabaWinRoute> _labaWinRouteRepository;
        private readonly IRepository<LabaMultiple> _labaMultipleRepository;
        private readonly IRepository<LabaOrderDetail> _labaOrderDetailRepository;
        private readonly IRepository<LabaOrder> _labaOrderRepository;
        private readonly IRepository<UserAssets> _userAssetsRepository;
        private readonly ICacheManager _cacheManager;
        public IEventBus EventBus { get; set; }


        public AbpQuartzLabaCalResultJob(IRepository<LabaList> labaListRepository, 
            IRepository<LabaWinRoute> labaWinRouteRepository,
            IRepository<LabaMultiple> labaMultipleRepository,
            IRepository<LabaOrderDetail> labaOrderDetailRepository,
            IRepository<LabaOrder> labaOrderRepository,
            IRepository<UserAssets> userAssetsRepository,
            ICacheManager cacheManager)
        {
            this._labaListRepository = labaListRepository;
            this._labaWinRouteRepository = labaWinRouteRepository;
            this._labaMultipleRepository = labaMultipleRepository;
            this._labaOrderDetailRepository = labaOrderDetailRepository;
            this._labaOrderRepository = labaOrderRepository;
            this._userAssetsRepository = userAssetsRepository;
            this.EventBus = NullEventBus.Instance;
            this._cacheManager = cacheManager;
        }

        [UnitOfWork]
        public override async Task Execute(IJobExecutionContext context)
        {
            try
            {
                JobDataMap dataMap = context.JobDetail.JobDataMap;

                var userAssets = await _userAssetsRepository.SingleAsync(u => u.CreatorUserId == (long)dataMap.Get("UserId"));
                var labaOrder = await _labaOrderRepository.FirstOrDefaultAsync(s => s.Id == (int)dataMap.Get("OrderId") && s.Status == 0);
                var labaOrderDetail = await _labaOrderDetailRepository.GetAllListAsync(l => l.OrderId == (int)dataMap.Get("OrderId") && l.Status == 0);
                var labaWinRoute = await _labaWinRouteRepository.GetAllListAsync(r => r.Status == 0 && labaOrderDetail.Select(l => l.RouteId).Contains(r.Id));

                var  labaList = _cacheManager
                                .GetCache("LabaCache")
                                .Get("LabaList", () => _labaListRepository.GetAllList());
                var labaMultiple = _cacheManager
                                .GetCache("LabaCache")
                                .Get("LabaMultiple", () => _labaMultipleRepository.GetAllList());

                if (labaOrder == null || labaOrderDetail.Count() == 0)
                {
                    //LogHelper.WriteLog("D:\\LabaError\\", "订单ID：" + data.Id + "  " + "错误信息：拉霸訂單失效");
                }

                string[] positions = dataMap.Get("Position").ToString().Split('|');

                int x = 0;
                Random random = new Random();
                var list = new List<List<LabaList>>();
                while (4 >= x)
                {
                    var listY = labaList.ToList().Where(l => l.X == x).ToList();
                    listY.AddRange(listY);
                    list.Add(listY.Skip(int.Parse(positions[x]) - 1).Take(3).ToList());
                    x++;
                }


                // 中奖的字符串
                string result = "";
                // 属性序列
                int index = 1;
                // 单条线路字符和次数
                int winCount = 1;
                string winStr = "";
                int winAmount = 0;
                foreach (var route in labaWinRoute)
                {
                    result = "";
                    index = 1;
                    winCount = 1;
                    foreach (var item in list)
                    {
                        result += item[(int)route.GetType().GetProperty("Y" + index).GetValue(route, null)].Content;
                        index++;
                    }

                    char[] strList = result.ToCharArray();

                    winStr = strList[0].ToString();
                    for (int strI = 1; strI < strList.Length; strI++)
                    {
                        if (strList[0] == strList[strI] || strList[strI] == 'W')
                        {
                            winCount++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    var detail = labaOrderDetail.Single(o => o.RouteId == route.Id);
                    //成功
                    if (winCount > 2)
                    {
                        foreach (var multiple in labaMultiple)
                        {
                            if (multiple.Content == winStr && multiple.Multiple == winCount)
                            {
                                winAmount += multiple.Amount * detail.Amount;
                                detail.WinAmount = multiple.Amount * detail.Amount;
                                detail.WinContent = multiple.Content;
                                break;
                            }
                        }
                    }

                    detail.Status = 1;
                }

                labaOrder.Status = 1;
                labaOrder.WinAmount = winAmount;
                userAssets.GoldCoin += winAmount;
            }
            catch (Exception ex)
            {
                EventBus.Trigger(new AbpHandledExceptionData(ex));

            }

            bool res = await Task<bool>.Run(() =>
            {
                return true;
            });
            
        }
    }
}
