using Lending.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Services
{
    public interface INotificationService
    {
        Task<Notification> CreateAsync(Notification notification);
        Task<IEnumerable<Notification>> GetNotificationsByUserAsync(int userId);
        Task<bool> MarkAsReadAsync(int notificationId);
        Task<IEnumerable<Notification>> GetPendingNotificationsAsync();
        Task SendRepaymentReminderAsync();
    }
}
