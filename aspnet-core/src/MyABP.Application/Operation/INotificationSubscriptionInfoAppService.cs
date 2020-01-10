using Abp.Application.Services;
using MyABP.Operation.Dto.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyABP.Operation
{
    public interface INotificationSubscriptionInfoAppService: IAsyncCrudAppService<NotificationSubscriptionInfoDto, Guid, PagedAndSortedNotificationSubscriptionInfoResultRequestDto>
    {

    }
}
