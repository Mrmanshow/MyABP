using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyABP.Operation.Dto.Notifications
{
    public class PagedAndSortedNotificationSubscriptionInfoResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }

        public string NotificationName { set; get; }
    }
}
