using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyABP.Operation.Dto.Notifications
{
    public class NotificationsTemplateMapProfile: Profile
    {
        public NotificationsTemplateMapProfile()
        {
            CreateMap<NotificationsTemplateDto, NotificationsTemplate>();
        }
    }
}
