using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Notifications;
using MyABP.Authorization.Roles;
using MyABP.Authorization.Users;
using MyABP.Operation.Dto.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyABP.Operation
{
    public class NotificationSubscriptionInfoAppService : AsyncCrudAppService<NotificationSubscriptionInfo, NotificationSubscriptionInfoDto, Guid, PagedAndSortedNotificationSubscriptionInfoResultRequestDto>, INotificationSubscriptionInfoAppService
    {
        private readonly IRepository<NotificationSubscriptionInfo, Guid> _repository;
        private readonly IRepository<User, long> _userRepository;
        private readonly RoleManager _roleManager;
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;

        public NotificationSubscriptionInfoAppService(IRepository<NotificationSubscriptionInfo, Guid> repository,
            IRepository<User, long> userRepository,
            RoleManager roleManager,
            INotificationSubscriptionManager notificationSubscriptionManager)
            : base(repository)
        {
            this._repository = repository;
            this._userRepository = userRepository;
            this._roleManager = roleManager;
            this._notificationSubscriptionManager = notificationSubscriptionManager;
        }


        protected override IQueryable<NotificationSubscriptionInfo> CreateFilteredQuery(PagedAndSortedNotificationSubscriptionInfoResultRequestDto input)
        {
            return _repository.GetAll()
                .Where(x => x.NotificationName == input.NotificationName);
        }

        protected NotificationSubscriptionInfoDto MapToEntityForUserDto(NotificationSubscriptionInfoForUser noti)
        {
            var roles = _roleManager.Roles.Where(r => noti.Roles.Any(ur => ur.RoleId == r.Id)).Select(r => r.DisplayName);
            var userDto = base.MapToEntityDto(noti);
            userDto.RoleNames = String.Join("|", roles.ToArray());
            return userDto;
        }

        public override async Task<PagedResultDto<NotificationSubscriptionInfoDto>> GetAll(PagedAndSortedNotificationSubscriptionInfoResultRequestDto input)
        {
            CheckGetAllPermission();

            var query = CreateFilteredQuery(input);
            var list = query.ToList();
            query = from s in query
                    join u in _userRepository.GetAll()
                    on s.UserId equals u.Id
                    where input.Keyword.IsNullOrWhiteSpace() ? true : u.UserName.Contains(input.Keyword)
                    select new NotificationSubscriptionInfoForUser
                    {
                        UserId = u.Id,
                        Roles = u.Roles,
                        UserName = u.UserName,
                        NotificationName = s.NotificationName
                    };

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            List<NotificationSubscriptionInfoForUser> entities = await AsyncQueryableExecuter.ToListAsync(query as IQueryable<NotificationSubscriptionInfoForUser>);

            return new PagedResultDto<NotificationSubscriptionInfoDto>(
                totalCount,
                entities.Select(MapToEntityForUserDto).ToList()
            );
        }

        public async Task Unsubscribe(NotificationSubscriptionInfoDto input)
        {
            await this._notificationSubscriptionManager.UnsubscribeAsync(new UserIdentifier(null, input.UserId), input.NotificationName);
        }
    }
}
