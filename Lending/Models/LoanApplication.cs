using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lending.Models
{
    public enum LoanStatus { PENDING, APPROVED, REJECTED, CLOSED }

    public class LoanApplication
    {
        [Key]
        public int LoanApplicationId { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int? LoanSchemeId { get; set; }
        public LoanScheme? LoanScheme { get; set; }

        public int? LoanOfficerId { get; set; }
        public LoanOfficer? LoanOfficer { get; set; }

        [Required]
        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;

        [Required]
        public LoanStatus Status { get; set; } = LoanStatus.PENDING;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Applied amount must be positive")]
        public decimal AppliedAmount { get; set; }

        [Required]
        [Range(1, 600, ErrorMessage = "Tenure must be at least 1 month")]
        public int TenureMonths { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Interest rate must be between 0 and 100%")]
        public decimal InterestRate { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; }

        public ICollection<Document>? Documents { get; set; }

        public Loan? Loan { get; set; }
    }
}