using Lending.Models;
using Lending.Repositories;
﻿using Lending.Data;
using Lending.Models;
using Lending.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lending.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IRepaymentRepository _repaymentRepository;

        public NotificationService(INotificationRepository notificationRepository, IRepaymentRepository repaymentRepository)
        {
            _notificationRepository = notificationRepository;
            _repaymentRepository = repaymentRepository;
        }

        public async Task<Notification> CreateAsync(Notification notification)
        {
            notification.SentDate = DateTime.UtcNow;
            notification.Status = NotificationStatus.Sent;
            return await _notificationRepository.CreateAsync(notification);
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByUserAsync(int userId)
        {

            var all = await _notificationRepository.GetAllAsync();
            return all.Where(n => n.CustomerId == userId);
            var allNotifications = await _notificationRepository.GetAllAsync();
            return allNotifications.Where(n => n.CustomerId == customerId);
        }

        public async Task<IEnumerable<Notification>> GetPendingNotificationsAsync()
        {
            var all = await _notificationRepository.GetAllAsync();
            return all.Where(n => n.Status == NotificationStatus.Failed);
        }

        // Instead of IsRead, we mark a notification as Sent to indicate it's "read"
        public async Task<bool> MarkAsReadAsync(int notificationId)
        {
            var existing = await _notificationRepository.GetByIdAsync(notificationId);
            if (existing == null)
                return false;

            existing.Status = NotificationStatus.Sent; // Mark as processed/read
            await _notificationRepository.EditAsync(existing);
            return true;
            var allNotifications = await _notificationRepository.GetAllAsync();
            return allNotifications.Where(n => n.Status == NotificationStatus.Failed);
        }

        public async Task SendRepaymentReminderAsync()
        {
            var today = DateTime.UtcNow;
            var reminderDate = today.AddDays(7).Date;

            var repayments = await _repaymentRepository.GetRepaymentsWithLoanAndCustomerAsync();

            var reminders = repayments
                .Where(r => r.DueDate.Date == reminderDate && r.Status == RepaymentStatus.PENDING)
                .ToList();

            foreach (var r in reminders)

            var repayments = await _repaymentRepository.GetRepaymentsWithLoanAndCustomerAsync();

            var remindersToSend = repayments
                .Where(r => r.DueDate.Date == reminderDate && r.Status == RepaymentStatus.PENDING)
                .ToList();

            foreach (var r in remindersToSend)
            {
                var notification = new Notification
                {
                    CustomerId = r.Loan.LoanApplication.CustomerId,
                    LoanApplicationId = r.Loan.LoanApplicationId,
                    Message = $"Your loan installment of {r.AmountDue:C} is due on {r.DueDate:dd/MM/yyyy}.",

                    Message = $"Your loan installment of {r.AmountDue:C} is due on {r.DueDate:dd/MM/yyyy}. Please ensure timely payment.",
                    SentDate = DateTime.UtcNow,
                    Status = NotificationStatus.Sent
                };

                await _notificationRepository.CreateAsync(notification);
            }
        }
    }
}