using System.ComponentModel.DataAnnotations;

namespace Lending.Models
{
    public enum ReportType
    {
        LoanStatus,
        NPAReport,
        CustomerReport,
        RepaymentReport
    }

    public class Report
    {
        [Key]
        public int ReportId { get; set; }

        [Required]
        public ReportType Type { get; set; }

        [Required(ErrorMessage = "Report title is required")]
        [StringLength(200)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Report URL or content is required")]
        [StringLength(1000)]
        public string Url { get; set; } // Could be a file URL or cloud storage link

        [Required]
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public int? GeneratedById { get; set; } // Admin user who generated
        public virtual LoanAdmin? GeneratedBy { get; set; }
    }
}
