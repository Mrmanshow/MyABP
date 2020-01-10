
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyABP.Operation.Dto.Notifications
{
    [AutoMapFrom(typeof(NotificationSubscriptionInfoForUser))]
    public class NotificationSubscriptionInfoDto: EntityDto<Guid>
    {
        [StringLength(256)]
        public string UserName { set; get; }

        public string RoleNames { get; set; }

        public long UserId { get; set; }

        [StringLength(96)]
        public string NotificationName { get; set; }
    }
}
