using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyABP.Operation.Dto.Notifications
{
    [AutoMapFrom(typeof(NotificationsTemplate))]
    public class NotificationsTemplateDto: EntityDto<int>
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
