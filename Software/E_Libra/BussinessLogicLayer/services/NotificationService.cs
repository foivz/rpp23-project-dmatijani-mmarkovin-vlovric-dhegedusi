﻿using DataAccessLayer.Repositories;
using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.services
{
    public class NotificationService
    {
        public List<Notification> GetAllNotificationByLibrary(int id)
        {
            using (var notificationsRepo = new NotificationsRepository())
            {
                return notificationsRepo.GetAllNotificationsForLibrary(id).ToList();
            }
        }
        public bool AddNewNotification(Notification notification)
        {
            using (var notificationsRepo = new NotificationsRepository())
            {
                var added = notificationsRepo.Add(notification);
                if (added != 0) return true;
            }
            return false;
        }
    }
}
