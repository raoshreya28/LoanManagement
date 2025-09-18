using System;
using System.ComponentModel.DataAnnotations;

namespace Lending.Models
{
    public enum UserRole
    {
        LOANADMIN,
        LOANOFFICER,
        CUSTOMER,
        SYSTEM
    }

    public abstract class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Enter the Full Name")]
        [StringLength(150, ErrorMessage = "Name cannot exceed 150 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "User Email is required")]
        [EmailAddress(ErrorMessage = "Email not in proper format")]
        [StringLength(150)]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        [MaxLength(100)]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public UserRole Role { get; set; }

        [RegularExpression(@"^[0-9]{3}-[0-9]{3}-[0-9]{4}$", ErrorMessage = "Phone must be in format 000-000-0000")]
        public string UserPhone { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
