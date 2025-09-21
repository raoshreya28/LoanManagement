using System.ComponentModel.DataAnnotations;

namespace Lending.Models
{
    public class LoanOfficer : User
    {
        [Required(ErrorMessage = "Branch name is required")]
        [StringLength(100, ErrorMessage = "Branch name cannot exceed 100 characters")]
        public string LoanOfficerBranch { get; set; }

        [Required(ErrorMessage = "City is required for officer auto-assignment")]
        [StringLength(50, ErrorMessage = "City name cannot exceed 50 characters")]
        public string LoanOfficerCity { get; set; } // used for auto-assignment

        [Required]
        public bool IsAvailable { get; set; } = true;

        [Range(0, int.MaxValue, ErrorMessage = "Assignments must be a non-negative number")]
        public int CurrentAssignments { get; set; } = 0;

    }
}
