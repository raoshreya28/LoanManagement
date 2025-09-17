using System.ComponentModel.DataAnnotations;

namespace Lending.Models
{
    public class LoanScheme
    {
        [Key]
        public int LoanSchemeId { get; set; }

        [Required(ErrorMessage = "Loan scheme name is required")]
        [StringLength(100, ErrorMessage = "Loan scheme name cannot exceed 100 characters")]
        public string LoanSchemeName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Interest rate is required")]
        [Range(0, 100, ErrorMessage = "Interest rate must be between 0 and 100%")]
        public decimal InterestRate { get; set; }

        [Required(ErrorMessage = "Maximum loan amount is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Maximum amount must be positive")]
        public decimal MaximumAmount { get; set; }

        [Required(ErrorMessage = "Minimum loan amount is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Minimum amount must be positive")]
        public decimal MinimumAmount { get; set; }

        [Required(ErrorMessage = "Maximum tenure in months is required")]
        [Range(1, 600, ErrorMessage = "Maximum tenure must be between 1 and 600 months")]
        public int MaximumTenureMonths { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation property
        public ICollection<LoanApplication>? LoanApplications { get; set; }
    }
}
