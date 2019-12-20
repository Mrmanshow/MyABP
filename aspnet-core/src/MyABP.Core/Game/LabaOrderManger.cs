using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using MyABP.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyABP.Game
{
    public class LabaOrderManger : DomainService, ILabaOrderManger
    {
        private readonly IRepository<UserAssets> _userAssetsRepository;
        private readonly IRepository<LabaOrder> _labaOrderRepository;
        private readonly IRepository<LabaWinRoute> _labaWinRouteRepository;
        private readonly IRepository<LabaOrderDetail> _labaOrderDeatilRepository;
        private readonly IRepository<LabaList> _labaListRepository;
        private readonly ICacheManager _cacheManager;

        public LabaOrderManger(IRepository<UserAssets> userAssetsRepository,
            IRepository<LabaOrder> labaOrderRepository,
            IRepository<LabaWinRoute> labaWinRouteRepository,
            IRepository<LabaOrderDetail> labaOrderDeatilRepository,
            IRepository<LabaList> labaListRepository,
            ICacheManager cacheManager)
        {
            this._userAssetsRepository = userAssetsRepository;
            this._labaOrderRepository = labaOrderRepository;
            this._labaWinRouteRepository = labaWinRouteRepository;
            this._labaOrderDeatilRepository = labaOrderDeatilRepository;
            this._labaListRepository = labaListRepository;
            this._cacheManager = cacheManager;
        }

        public async Task<int> CreateLabaOrder(LabaOrder order, string routes)
        {
            var userAssets = await _userAssetsRepository.FirstOrDefaultAsync(x => x.CreatorUserId == order.CreatorUserId);
            if (userAssets.GoldCoin < order.Amount)
            {
                return -1;
            }

            var routeList = routes.Split('|');
            order.Count = routeList.Length;

            var labaWinRoute = _cacheManager
                                .GetCache("LabaCache")
                                .Get("LabaWinRoute", () => _labaWinRouteRepository.GetAllList(x => x.Status == 0));



            foreach (var item in routeList)
            {
                int routeId = int.Parse(item.Split('-')[0]);
                var route = labaWinRoute.Where(x => x.Id == routeId);

                if (route == null)
                {
                    return -2;
                }

            }


            await _labaOrderRepository.InsertAndGetIdAsync(order);

            foreach (var item in routeList)
            {
                int routeId = int.Parse(item.Split('-')[0]);
                var labaDeatil = new LabaOrderDetail
                {
                    CreatorUserId = order.CreatorUserId,
                    OrderId = order.Id,
                    RouteId = routeId,
                    Amount = int.Parse(item.Split('-')[1])
                };
                await _labaOrderDeatilRepository.InsertAsync(labaDeatil);

            }

            userAssets.GoldCoin -= order.Amount;

            return order.Id;
        }

        public async Task<string> LabaOrderCal(int orderId)
        {
            string winYResult = "";

            var labaList = _cacheManager
                                .GetCache("LabaCache")
                                .Get("LabaList", () => _labaListRepository.GetAllList());

            int x = 0;
            int y = 0;
            Random random = new Random();
            var postion = new List<LabaList>();
            while (4 >= x)
            {
                var listY = labaList.Where(l => l.X == x).ToList();
                y = random.Next(listY.Count());
                winYResult += y.ToString() + "|";
                listY.AddRange(listY);
                postion.AddRange(listY.Skip(y - 1).Take(3));
                x++;
            }

            var order = await _labaOrderRepository.SingleAsync(l => l.Id == orderId);
            order.Position = winYResult.Substring(0, winYResult.Length - 1);

            return order.Position;
        }
    }
}
