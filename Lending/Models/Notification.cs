using System;
using System.ComponentModel.DataAnnotations;

namespace Lending.Models
{
    public enum NotificationStatus { Sent, Failed }

    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }

        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public int? LoanApplicationId { get; set; }
        public LoanApplication? LoanApplication { get; set; }

        [Required]
        [StringLength(1000)]
        public string Message { get; set; }

        public DateTime SentDate { get; set; } = DateTime.UtcNow;

        [Required]
        public NotificationStatus Status { get; set; } = NotificationStatus.Sent;
    }
}
