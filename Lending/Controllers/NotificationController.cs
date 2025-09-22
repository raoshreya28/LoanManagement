using Lending.Models;
using Lending.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetByUser(int userId)
        {
            var notifications = await _notificationService.GetNotificationsByUserAsync(userId);
            return Ok(notifications);
        }

        [HttpPost]
        public async Task<ActionResult<Notification>> Create([FromBody] Notification notification)
        {
            var created = await _notificationService.CreateAsync(notification);
            return CreatedAtAction(nameof(GetByUser), new { userId = created.CustomerId }, created);
        }

        [HttpPost("{id}/mark-read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var success = await _notificationService.MarkAsReadAsync(id);
            if (!success)
                return NotFound($"Notification {id} not found.");

            return Ok($"Notification {id} marked as read.");
        }
    }
}
