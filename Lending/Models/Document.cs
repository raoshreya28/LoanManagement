using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lending.Models
{
    public enum DocumentType
    {
        Aadhaar,
        PAN,
        BankStatement,
        IncomeProof,
        Other
    }

    public class Document
    {
        [Key]
        public int DocumentId { get; set; }

        // Customer is required
        [Required]
        public int CustomerId { get; set; }   // Not nullable
        public virtual Customer Customer { get; set; }  // navigation property

        // LoanApplication is optional
        public int? LoanApplicationId { get; set; }
        public virtual LoanApplication? LoanApplication { get; set; } // optional navigation

        [Required]
        public DocumentType Type { get; set; }

        [Required(ErrorMessage = "Document URL is required")]
        [StringLength(500)]
        public string Url { get; set; } // Store Cloudinary URL

        [Required]
        public bool IsVerified { get; set; } = false;

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public DateTime? VerifiedAt { get; set; }
    }
}
