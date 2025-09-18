using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lending.Models
{
    public class Customer : User
    {
        // navigation properties
        public ICollection<LoanApplication>? LoanApplications { get; set; }
        public ICollection<Document>? Documents { get; set; }

        [StringLength(12, ErrorMessage = "Aadhaar number cannot exceed 12 digits")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Aadhaar must be exactly 12 digits")]
        public string AadhaarNumber { get; set; }

        [StringLength(10, ErrorMessage = "PAN must be exactly 10 characters")]
        [RegularExpression(@"^[A-Z]{5}[0-9]{4}[A-Z]$", ErrorMessage = "Invalid PAN format")]
        public string PanNumber { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Annual income must be a positive value")]
        public decimal? AnnualIncome { get; set; }

        [StringLength(100, ErrorMessage = "Occupation cannot exceed 100 characters")]
        public string Occupation { get; set; }

        [Required(ErrorMessage = "Bank account number is required")]
        [StringLength(20, ErrorMessage = "Bank account number cannot exceed 20 digits")]
        [RegularExpression(@"^\d{9,20}$", ErrorMessage = "Bank account number must be between 9 and 20 digits")]
        public string BankAccountNumber { get; set; }

        [Required(ErrorMessage = "IFSC code is required")]
        [RegularExpression(@"^[A-Z]{4}0[A-Z0-9]{6}$", ErrorMessage = "Invalid IFSC format")]
        public string CustomerIFSC { get; set; }

        [Required(ErrorMessage = "Customer city is required")]
        [StringLength(50, ErrorMessage = "City name cannot exceed 50 characters")]
        public string CustomerCity { get; set; }
    }
}
