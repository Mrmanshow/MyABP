using Abp;
using Abp.Domain.Entities;
using Abp.Localization;
using Abp.Notifications;
using MyABP.Game;
using MyABP.Notification.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyABP.Notification
{
    public class NotificationPublisherAppService: MyABPAppServiceBase, INotificationPublisherAppService
    {
        private readonly INotificationPublisher _notificationPublisher;
        private readonly IUserNotificationManager _userNotificationManager;
        private readonly IRealTimeNotifier _realTimeNotifier;

        public NotificationPublisherAppService(INotificationPublisher notificationPublisher,
            IUserNotificationManager userNotificationManager,
            IRealTimeNotifier realTimeNotifier)
        {
            _notificationPublisher = notificationPublisher;
            _userNotificationManager = userNotificationManager;
            _realTimeNotifier = realTimeNotifier;
        }

        public async Task PushRealTimeNotifications()
        {
            List<UserNotification> list = await _userNotificationManager.GetUserNotificationsAsync(new UserIdentifier(null, 3));
            await _realTimeNotifier.SendNotificationsAsync(list.ToArray());
        }

            //Send a general notification to a specific user
            public async Task Publish_SentFrendshipRequest(string senderUserName, string friendshipMessage, UserIdentifier targetUserId)
        {
            await _notificationPublisher.PublishAsync("SentFrendshipRequest", new SentFrendshipRequestNotificationDataInputDto(senderUserName, friendshipMessage), userIds: new[] { targetUserId });
        }

        //Send an entity notification to a specific user
        //public async Task Publish_CommentPhoto(string commenterUserName, string comment, Guid photoId, UserIdentifier photoOwnerUserId)
        //{
        //    await _notificationPublisher.PublishAsync("CommentPhoto", new CommentPhotoNotificationData(commenterUserName, comment), new EntityIdentifier(typeof(LabaOrder), photoId), userIds: new[] { photoOwnerUserId });
        //}

        //Send a general notification to all subscribed users in current tenant (tenant in the session)
        public async Task Publish_LowDisk(int remainingDiskInMb)
        {
            //Example "LowDiskWarningMessage" content for English -> "Attention! Only {remainingDiskInMb} MBs left on the disk!"
            var data = new LocalizableMessageNotificationData(new LocalizableString("LowDiskWarningMessage", "MyLocalizationSourceName"));
            data["remainingDiskInMb"] = remainingDiskInMb;

            await _notificationPublisher.PublishAsync("System.LowDisk", data, severity: NotificationSeverity.Warn);
        }
    }
}
