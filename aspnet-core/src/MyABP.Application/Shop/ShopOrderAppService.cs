using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using MyABP.Authorization;
using MyABP.Authorization.Users;
using MyABP.Redis;
using MyABP.Shop.Dto;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyABP.Shop
{
    [AbpAuthorize(PermissionNames.Pages_Shop)]
    public class ShopOrderAppService: AsyncCrudAppService<ProductOrder, ProductOrderDto, int, PagedProductOrderResultRequestDto, ShopOrderCreateInput>, IShopOrderAppService
    {
        private readonly IRepository<ProductOrder> _productOrderRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<UserAssets> _userAssetsRepository;
        private readonly IRepository<Address> _addressRepository;
        private readonly IAbpSession _abpSession;
        private readonly IRedisManager<EntityDto<int>> _redisManager;

        public ShopOrderAppService(IRepository<ProductOrder> productOrderRepository,
            IRepository<Product> productRepository,
            IRepository<UserAssets> userAssetsRepository,
            IRepository<Address> addressRepository,
            IAbpSession abpSession,
            IRedisManager<EntityDto<int>> redisManager)
            :base(productOrderRepository)
        {
            this._productOrderRepository = productOrderRepository;
            this._productRepository = productRepository;
            this._userAssetsRepository = userAssetsRepository;
            this._addressRepository = addressRepository;
            this._abpSession = abpSession;
            this._redisManager = redisManager;

        }

        protected override IQueryable<ProductOrder> CreateFilteredQuery(PagedProductOrderResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Address, x => x.User)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.ProductName.Contains(input.Keyword) || 
                x.OrderCode.Contains(input.Keyword) || x.User.UserName.Contains(input.Keyword));
        }

        public override async Task<ProductOrderDto> Create(ShopOrderCreateInput input)
        {
            CheckCreatePermission();

            if (input.Count < 1)
            {
                throw new UserFriendlyException("Passwords must be at least 8 characters, contain a lowercase, uppercase, and number.");
            }

            if (input.PayIntegration < 0)
            {
                //下架
                throw new UserFriendlyException("Passwords must be at least 8 characters, contain a lowercase, uppercase, and number.");
            }

            var product = await _productRepository.FirstOrDefaultAsync(p => p.Status == 0 && p.Id == input.ProductId);
            if (product == null || product?.Stock < product.SoldCount + input.Count)
            {
                //库存不足
                throw new UserFriendlyException(1,"Passwords must be at least 8 characters, contain a lowercase, uppercase, and number.");
            }

            decimal payBalance = 0;
            decimal amount = product.Amount* input.Count + product.Freight;
            if (amount > input.PayIntegration)
            {
                payBalance = amount - input.PayIntegration;
            }

            var userAssets = await _userAssetsRepository.SingleAsync(u => u.CreatorUserId == _abpSession.UserId);
            if (userAssets.GoldCoin < input.PayIntegration)
            {
                //余额不足
                throw new UserFriendlyException("Passwords must be at least 8 characters, contain a lowercase, uppercase, and number.");
            }

            if (input.AddressId == 0 && product.Type == 1)
            {
                //未填写地址
                throw new UserFriendlyException("Passwords must be at least 8 characters, contain a lowercase, uppercase, and number.");
            }
            else if (input.AddressId != 0 && product.Type == 1)
            {
                //查询有效地址
                if (await _addressRepository.FirstOrDefaultAsync(a => a.UserId == _abpSession.UserId && a.Id == input.AddressId && a.Status != 2) == null)
                {
                    throw new UserFriendlyException("Passwords must be at least 8 characters, contain a lowercase, uppercase, and number.");
                }
            }

            var entity = MapToEntity(input);

            Random radom = new Random();
            entity.OrderCode = DateTime.Now.ToString("yyyyMMddHHmmss") + radom.Next(1000, 9999);
            entity.UserId = _abpSession.UserId.Value;
            entity.ProductType = product.Type;
            entity.Freight = product.Freight;
            entity.Amount = int.Parse(amount.ToString());
            entity.Price = product.Amount;
            entity.ProductName = product.Name;
            await Repository.InsertAsync(entity);

            product.SoldCount += input.Count;

            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }

        public override async Task<ProductOrderDto> Get(EntityDto<int> input)
        {
            // 测试Redis
            this._redisManager.SetValue("test", input);
            this._redisManager.StringSet("test", input.Id);

            return await base.Get(input);
        }
    }
}
