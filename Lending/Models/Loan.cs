using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lending.Models
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }

        [Required]
        public int? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        [Required]
        public int? LoanApplicationId { get; set; }
        public virtual LoanApplication? LoanApplication { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrincipalAmount { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal InterestRate { get; set; }

        [Required]
        [Range(1, 600)]
        public int TenureMonths { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        // Navigation property
        public ICollection<Repayment>? Repayments { get; set; }
    }
}
