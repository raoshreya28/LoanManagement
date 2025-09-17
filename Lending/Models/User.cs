using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Lending.Models
{
    public enum UserRole
    { LOANADMIN, LOANOFFICER, CUSTOMER, SYSTEM }

    public abstract class User      //abstract so we can't create User directly
    {
        [Key]
        public int UserId { get; set; }

        //Full Name
        [Required(ErrorMessage = "Enter the Full Name")]
        [StringLength(150, ErrorMessage = "Name cannot exceed 150 characters")]
        public string UserName { get; set; }

        //Email
        [Required(ErrorMessage = "User Email is required")]
        [EmailAddress(ErrorMessage = "Email not in proper format")]
        [StringLength(150)]
        public string UserEmail { get; set; }

        //Password (hash stored)
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        [MaxLength(100)]
        public string PasswordHash { get; set; }  // Store hashed password, not plain text

        //Role
        [Required(ErrorMessage = "Role is required")]
        public UserRole Role { get; set; }

        //Phone Number
        [RegularExpression(@"^[0-9]{3}-[0-9]{3}-[0-9]{4}$",
            ErrorMessage = "Phone must be in format 000-000-0000")]
        public string UserPhone { get; set; }

        //Address
        [StringLength(250)]
        public string Address { get; set; }

        //Created / Updated Dates
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTime? UpdatedAt { get; set; }

        //Soft delete / Active flag
        public bool IsActive { get; set; } = true;

    }
}

