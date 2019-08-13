using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Messages
{
    //TODO: Can Create handlers for sending email or sms or mobile notification
    public class UserNotificationMessage : INotification
    {
        public string Message { get; set; }
        public string Subject { get; set; }
        public DateTimeOffset WhenToSend { get; set; }
        public int SenderUserID { get; set; }
        //TODO: Add NotificationType enum to this entity
    }
}
