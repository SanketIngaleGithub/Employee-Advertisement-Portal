using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EmployeeAdvertisementPortal
{
    public class EmployeeViewModel
    {
        public int EmpId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("First Name")]
        [RegularExpression(@"^[a-z A-Z]+$", ErrorMessage="Enter only alphabets.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Last Name")]
        [RegularExpression(@"^[a-z A-Z]+$", ErrorMessage = "Enter only alphabets.")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "This field is required")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Incorrect Email Format")]
        [StringLength(40, ErrorMessage = "Please do not enter values over 40 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DOB { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Role")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Created By")]
        public int CreatedBy { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Created Date")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Modified By")]
        public int ModifiedBy { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Modified Date")]
        [DataType(DataType.Date)]
        public DateTime ModifiedDate { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }

        public IEnumerable<SelectListItem> Role { get; set; }
    }
}
