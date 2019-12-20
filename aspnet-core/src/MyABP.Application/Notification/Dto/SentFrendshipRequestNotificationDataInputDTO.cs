using Abp.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyABP.Notification.DTO
{
    public class SentFrendshipRequestNotificationDataInputDto : NotificationData
    {
        public string SenderUserName { get; set; }

        public string FriendshipMessage { get; set; }

        public SentFrendshipRequestNotificationDataInputDto(string senderUserName, string friendshipMessage)
        {
            SenderUserName = senderUserName;
            FriendshipMessage = friendshipMessage;
        }
    }
}
