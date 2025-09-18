using System.ComponentModel.DataAnnotations;

namespace Lending.Models
{
    public class LoanAdmin : User
    {
        public int AdminId { get; set; } 

        [Required(ErrorMessage = "Admin Department is required")]
        [StringLength(100, ErrorMessage = "Department name cannot exceed 100 characters")]
        public string AdminDepartment { get; set; } // Example: "Finance"
    }
}
