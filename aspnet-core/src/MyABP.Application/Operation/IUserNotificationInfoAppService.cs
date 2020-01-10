using Abp.Application.Services;
using Abp.Notifications;
using MyABP.Operation.Dto.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyABP.Operation
{
    public interface IUserNotificationInfoAppService : IAsyncCrudAppService<UserNotification, Guid, PagedAndSortedUserNotificationInfoResultRequestDto>
    {
    }
}
