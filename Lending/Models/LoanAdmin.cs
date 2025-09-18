using System.ComponentModel.DataAnnotations;

namespace Lending.Models
{
    public class LoanAdmin : User
    {
        [Required(ErrorMessage = "Admin Department is required")]
        [StringLength(100, ErrorMessage = "Department name cannot exceed 100 characters")]
        public string AdminDepartment { get; set; }
    }
}
