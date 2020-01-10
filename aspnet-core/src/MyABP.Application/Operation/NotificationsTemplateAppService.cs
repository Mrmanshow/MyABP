using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Notifications;
using Abp.UI;
using MyABP.Authorization;
using MyABP.Operation.Dto.Notifications;
using System.Linq;
using System.Threading.Tasks;

namespace MyABP.Operation
{
    [AbpAuthorize(PermissionNames.Pages_Operation)]
    public class NotificationsTemplateAppService : AsyncCrudAppService<NotificationsTemplate, NotificationsTemplateDto, int, PagedAndSortedNotificationsTemplateDtoResultRequestDto, NotificationsTemplateCreateInput, NotificationsTemplateUpdateInput>, INotificationsTemplateAppService
    {
        private readonly IRepository<NotificationsTemplate> _repositoryNotificationsTemplate;
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;

        public NotificationsTemplateAppService(IRepository<NotificationsTemplate> repositoryNotificationsTemplate,
            INotificationSubscriptionManager notificationSubscriptionManager)
            :base(repositoryNotificationsTemplate)
        {
            this._repositoryNotificationsTemplate = repositoryNotificationsTemplate;
            this._notificationSubscriptionManager = notificationSubscriptionManager;
        }

        protected override IQueryable<NotificationsTemplate> CreateFilteredQuery(PagedAndSortedNotificationsTemplateDtoResultRequestDto input)
        {
            return _repositoryNotificationsTemplate.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.NotificationName.Contains(input.Keyword) 
                || x.Content.Contains(input.Keyword) || x.DisplayName.Contains(input.Keyword));
        }

        public override async Task<NotificationsTemplateDto> Create(NotificationsTemplateCreateInput input)
        {
            var model = await _repositoryNotificationsTemplate.FirstOrDefaultAsync(x => x.NotificationName == input.NotificationName);
            if (model != null)
            {
                throw new UserFriendlyException(1, "通知名称已存在");
            }

            return await base.Create(input);
        }

        public override async Task Delete(EntityDto<int> input)
        {
            CheckDeletePermission();

            var notification = Repository.Get(input.Id);
            var list = await this._notificationSubscriptionManager.GetSubscriptionsAsync(notification.NotificationName);
            foreach (var item in list)
            {
                await this._notificationSubscriptionManager.UnsubscribeAsync(new UserIdentifier(null, item.UserId), notification.NotificationName);
            }
            
            await Repository.DeleteAsync(input.Id);
        }

    }
}
