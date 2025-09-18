using Lending.Data;
using Lending.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lending.Services
{
    public class NotificationService : INotificationService
    {
        private readonly AppDbContext _context;

        public NotificationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Notification> SendNotificationAsync(Notification notification)
        {
            notification.SentDate = DateTime.UtcNow;
            notification.Status = NotificationStatus.Sent;
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
            return notification;
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByCustomerAsync(int customerId)
        {
            return await _context.Notifications
                                 .Where(n => n.CustomerId == customerId)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetPendingNotificationsAsync()
        {
            return await _context.Notifications
                                 .Where(n => n.Status == NotificationStatus.Failed)
                                 .ToListAsync();
        }

        // Sends reminders 7 days before due date
        public async Task SendRepaymentReminderAsync()
        {
            var today = DateTime.UtcNow;
            var reminderDate = today.AddDays(7);

            var repayments = await _context.Repayments
                                           .Include(r => r.LoanApplication)
                                           .ThenInclude(l => l.Customer)
                                           .Where(r => r.DueDate.Date == reminderDate.Date && r.Status == RepaymentStatus.PENDING)
                                           .ToListAsync();

            foreach (var r in repayments)
            {
                var notification = new Notification
                {
                    CustomerId = r.LoanApplication.CustomerId,
                    LoanApplicationId = r.LoanApplicationId,
                    Message = $"Your loan installment of {r.AmountDue:C} is due on {r.DueDate:dd/MM/yyyy}. Please ensure timely payment.",
                    SentDate = DateTime.UtcNow,
                    Status = NotificationStatus.Sent
                };

                await _context.Notifications.AddAsync(notification);
            }

            await _context.SaveChangesAsync();
        }
    }
}
