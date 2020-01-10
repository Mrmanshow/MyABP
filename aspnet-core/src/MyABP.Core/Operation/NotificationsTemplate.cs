using Abp.Notifications;
using MyABP.Shop;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyABP.Operation
{
    [Table("AbpNotificationsTemplate")]
    public class NotificationsTemplate : AuditedEntity<int>
    {
        [Required]
        [StringLength(96)]
        public string NotificationName { set; get; }

        [StringLength(50)]
        public string DisplayName { set; get; }

        [StringLength(200)]
        public string Content { set; get; }

        public int SubscribedNumber { set; get; }

        public int PushNumber { set; get; }

        public int Status { set; get; }
    }
}
