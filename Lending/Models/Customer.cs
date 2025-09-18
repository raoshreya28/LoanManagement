using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lending.Models
{
    public class Customer : User
    {
        public int CustomerId { get; set; }

        // No extra key needed; inherited UserId is primary
        // Navigation for LoanApplications
        public ICollection<LoanApplication>? LoanApplications { get; set; }

        // Navigation for Documents
        public ICollection<Document>? Documents { get; set; }

        [StringLength(12)]
        [RegularExpression(@"^\d{12}$")]
        public string AadhaarNumber { get; set; }

        [StringLength(10)]
        [RegularExpression(@"^[A-Z]{5}[0-9]{4}[A-Z]$")]
        public string PanNumber { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? AnnualIncome { get; set; }

        [StringLength(100)]
        public string Occupation { get; set; }

        [Required, StringLength(20)]
        [RegularExpression(@"^\d{9,20}$")]
        public string BankAccountNumber { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]{4}0[A-Z0-9]{6}$")]
        public string CustomerIFSC { get; set; }

        [Required, StringLength(50)]
        public string CustomerCity { get; set; }
    }
}
