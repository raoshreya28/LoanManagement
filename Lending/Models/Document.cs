using System;
using System.ComponentModel.DataAnnotations;

namespace Lending.Models
{
    public enum DocumentType { Aadhaar, PAN, BankStatement, IncomeProof, Other }

    public class Document
    {
        [Key]
        public int DocumentId { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        // optional link to application — NULL when not attached or when app is deleted (we will restrict cascade)
        public int? LoanApplicationId { get; set; }
        public LoanApplication? LoanApplication { get; set; }

        [Required]
        public DocumentType Type { get; set; }

        [Required(ErrorMessage = "Document URL is required")]
        [StringLength(500)]
        public string Url { get; set; }

        [Required]
        public bool IsVerified { get; set; } = false;

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public DateTime? VerifiedAt { get; set; }
    }
}
