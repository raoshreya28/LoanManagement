using Lending.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Services
{
    public interface INotificationService
    {
        Task<Notification> SendNotificationAsync(Notification notification);
        Task<IEnumerable<Notification>> GetNotificationsByCustomerAsync(int customerId);
        Task<IEnumerable<Notification>> GetPendingNotificationsAsync();
        Task SendRepaymentReminderAsync();
    }
}
