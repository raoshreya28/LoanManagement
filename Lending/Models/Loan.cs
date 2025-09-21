using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lending.Models
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }

        [Required]
        public int LoanApplicationId { get; set; }
        public LoanApplication LoanApplication { get; set; }

        [Required]
        public decimal ApprovedAmount { get; set; }

        [Required]
        public int LoanOfficerId { get; set; }
        public LoanOfficer LoanOfficer { get; set; }

        [Required]
        public DateTime DisbursementDate { get; set; }

        [Required]
        public LoanStatus Status { get; set; } = LoanStatus.APPROVED;

        public ICollection<Repayment>? Repayments { get; set; }
    }
}