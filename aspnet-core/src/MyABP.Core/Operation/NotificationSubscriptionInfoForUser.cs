using Abp.Authorization.Users;
using Abp.Notifications;
using System;
using System.Collections.Generic;

namespace MyABP.Operation
{
    public class NotificationSubscriptionInfoForUser : NotificationSubscriptionInfo
    {
        public virtual string UserName { get; set; }

        public virtual ICollection<UserRole> Roles { set;get;}
    }
}
