using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyABP.Operation.Dto.Notifications
{
    [AutoMapTo(typeof(NotificationsTemplate))]
    public class NotificationsTemplateCreateInput
    {
        [Required]
        [StringLength(96)]
        public string NotificationName { set; get; }

        [StringLength(50)]
        public string DisplayName { set; get; }

        [StringLength(200)]
        public string Content { set; get; }

        public int Status { set; get; }
    }
}
