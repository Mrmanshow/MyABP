using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Notifications;
using Abp.Runtime.Session;
using MyABP.Authorization;
using MyABP.Operation.Dto.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyABP.Operation
{
    [AbpAuthorize(PermissionNames.Pages_Operation)]
    public class UserNotificationInfoAppService: AsyncCrudAppService<UserNotificationInfo, UserNotification, Guid, PagedAndSortedUserNotificationInfoResultRequestDto>, IUserNotificationInfoAppService
    {
        private readonly IRepository<UserNotificationInfo, Guid> _userNotificationInfoRepository;
        private readonly IUserNotificationManager _userNotificationManager;

        public UserNotificationInfoAppService(IUserNotificationManager userNotificationManager,
            IRepository<UserNotificationInfo, Guid> userNotificationInfoRepository)
            :base(userNotificationInfoRepository)
        {
            this._userNotificationManager = userNotificationManager;
            this._userNotificationInfoRepository = userNotificationInfoRepository;

            CreatePermissionName = PermissionNames.Pages_Users;
            UpdatePermissionName = PermissionNames.Pages_Users;
            DeletePermissionName = PermissionNames.Pages_Users;
        }

        public override async Task<PagedResultDto<UserNotification>> GetAll(PagedAndSortedUserNotificationInfoResultRequestDto input)
        {
            CheckGetAllPermission();

            var list = await this._userNotificationManager.GetUserNotificationsAsync(new UserIdentifier(null, AbpSession.UserId.Value), input.NotificationName, UserNotificationState.Unread, 0, input.MaxResultCount);
            var count = await this._userNotificationManager.GetUserNotificationCountAsync(new UserIdentifier(null, AbpSession.UserId.Value), input.NotificationName, UserNotificationState.Unread);
      
            return new PagedResultDto<UserNotification>(
                count,
                list
            );
        }
    }
}
