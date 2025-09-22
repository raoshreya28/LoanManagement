using Lending.Data;
using Lending.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;

        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notification>> GetAllAsync()
        {
            return await _context.Notifications
                                 .Include(n => n.Customer)
                                 .Include(n => n.LoanApplication)
                                 .ToListAsync();
        }

        public async Task<Notification?> GetByIdAsync(int id)
        {
            return await _context.Notifications
                                 .Include(n => n.Customer)
                                 .Include(n => n.LoanApplication)
                                 .FirstOrDefaultAsync(n => n.NotificationId == id);
        }

        public async Task<Notification> CreateAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
            return notification;
        }

        public async Task<Notification> EditAsync(Notification notification)
        {
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync();
            return notification;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _context.Notifications.FirstOrDefaultAsync(n => n.NotificationId == id);
            if (existing != null)
            {
                _context.Notifications.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
