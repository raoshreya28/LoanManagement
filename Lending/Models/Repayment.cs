using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lending.Models
{
    public enum RepaymentStatus { PENDING, PAID, OVERDUE }

    public class Repayment
    {
        [Key]
        public int RepaymentId { get; set; }

        [Required]
        public int? LoanApplicationId { get; set; }
        public LoanApplication? LoanApplication { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Amount due must be positive")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountDue { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue)]
        public decimal AmountPaid { get; set; } = 0;

        [Required]
        public DateTime DueDate { get; set; }

        public DateTime? PaidDate { get; set; }

        [Required]
        public RepaymentStatus Status { get; set; } = RepaymentStatus.PENDING;


    }
}
