using Abp;
using Abp.Domain.Entities;
using Abp.Notifications;
using MyABP.Game;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyABP.Notification
{
    public class NotificationSubscriptionAppService: MyABPAppServiceBase, INotificationSubscriptionAppService
    {
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;

        public NotificationSubscriptionAppService(INotificationSubscriptionManager notificationSubscriptionManager)
        {
            this._notificationSubscriptionManager = notificationSubscriptionManager;
        }

        //Subscribe to a general notification
        public async Task Subscribe_SentFriendshipRequest(int? tenantId, long userId)
        {
            await _notificationSubscriptionManager.SubscribeAsync(new UserIdentifier(tenantId, userId), "SentFriendshipRequest");
        }

        //Subscribe to an entity notification
        public async Task Subscribe_CommentPhoto(int? tenantId, long userId, Guid photoId)
        {
            await _notificationSubscriptionManager.SubscribeAsync(new UserIdentifier(tenantId, userId), "CommentPhoto", new EntityIdentifier(typeof(LabaOrder), photoId));
        }
    }
}
