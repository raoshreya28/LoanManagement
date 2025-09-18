using System;
using System.ComponentModel.DataAnnotations;

namespace Lending.Models
{
    public enum ReportType { LoanStatus, NPAReport, CustomerReport, RepaymentReport }

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
        public string Url { get; set; }

        [Required]
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

        public int? GeneratedById { get; set; }
        public LoanAdmin? GeneratedBy { get; set; }
    }
}
